﻿using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic;
using ClashRoyale.Game.Logic.Types;
using SFML.System;
using System;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public partial class EntityContext
    {
        public Arena Arena { get; set; }
        public Entity Entity { get; set; }
        public int Level { get; set; }
        public float DeployTime { get; set; }
        public float ReloadTime { get; set; }
        public float LoadTime { get; set; }
        
        
        public float Rotation { get; set; }
        private Vector2 MoveVelocity { get; set; }
        public Vector2 Velocity { get; set; }


        public Vector2 Acceleration { get; set; }
        public float EstimatedHitpoints { get; set; }
        public Dictionary<EntityContext, bool> TargetedBy { get; set; }
        public EntityContext? Target { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;

        private Action GetNearestEnemy { get; set; }

        private void Setup(Arena arena, Entity entity)
        {
            Arena = arena;
            Entity = entity;
            ReloadTime = 0;
            if (entity.EntityData.LoadFirstHit) // For things like Sparky
            {
                LoadTime = 0;
            }
            else
            {
                LoadTime = entity.EntityData.LoadTime;
            }
            Rotation = -90;
            DeployTime = entity.EntityData.DeployTime / 1000.0f;
            Velocity = new(0, 0);
            EstimatedHitpoints = entity.EntityData.Hitpoints;
            TargetedBy = new();
            if (entity.EntityData.AttacksGround && entity.EntityData.AttacksAir)
            {
                GetNearestEnemy = GetNearestAnyEnemy;
            }
            else if (entity.EntityData.AttacksGround)
            {
                GetNearestEnemy = GetNearestGroundEnemy;
            }
            else if (entity.EntityData.AttacksAir)
            {
                GetNearestEnemy = GetNearestAirEnemy;
            }
        }
        public EntityContext(Arena arena, EntityData entityData, Vector2 position, int level = 0)
        {
            Setup(arena, new Entity(entityData, entityData.Hitpoints, position, Team.Blue));
            Level = level;
        }
        public EntityContext(Arena arena, Entity entity)
        {
            Setup(arena, entity);
        }
        private void RemoveTarget()
        {
            LoadTime = Entity.EntityData.LoadTime / 1000.0f;
            Target!.TargetedBy.Remove(this);
            Target = null;
        }
        private float GetCombinedRangeRadius(EntityContext entityContext)
        {
            return this.Entity.EntityData.Range + this.Entity.EntityData.CollisionRadius + entityContext.Entity.EntityData.CollisionRadius;
        }
        public void Tick(GameTime gameTime)
        {
            if (this.DeployTime > 0)
            {
                this.DeployTime -= gameTime.DeltaTime;
                return;
            }
            if (this.Target != null)
            {
                if (this.Target.EstimatedHitpoints <= 0)
                {
                    RemoveTarget();
                }
                else
                {
                    double distance = GetDistanceBetweenPoints(this.Entity.Position, this.Target.Entity.Position);
                    if (distance > this.Entity.EntityData.Range + this.Entity.EntityData.CollisionRadius + this.Target.Entity.EntityData.CollisionRadius)
                    {
                        RemoveTarget();
                    }
                }
            }
            if (this.Target == null)
            {
                GetNearestEnemy.DynamicInvoke();
            }
            if (this.Target != null)
            {
                LookAtTarget(gameTime, out float angle);
                if (this.Rotation == angle)
                {
                    double distance = GetDistanceBetweenPoints(this.Entity.Position, this.Target.Entity.Position);
                    if (distance <= GetCombinedRangeRadius(this.Target)) // Close enough to attack
                    {
                        if (this.LoadTime != 0) // This will be zero once it starts attacking
                        {
                            this.ReloadTime = (this.Entity.EntityData.HitSpeed / 1000.0f) - this.LoadTime;
                            this.LoadTime = 0;
                        }
                        FireAtTarget(gameTime);
                    }
                    else
                    {
                        ApproachTarget(gameTime);
                    }
                }
            }
            else
            {
                LoadTime = Math.Min(LoadTime + gameTime.DeltaTime, Entity.EntityData.LoadTime / 1000.0f);
                Move(gameTime);
            }
        }
        private void LookAtTarget(GameTime gameTime, out float angle)
        {
            Vector2 direction = this.Target!.Entity.Position - this.Entity.Position;
            angle = (float)(Math.Atan2(direction.Y, direction.X) * (180.0 / Math.PI));
            if (this.Rotation != angle)
            {
                if (this.Entity.EntityData.RotateAngleSpeed > 0)
                {
                    if (this.Rotation > angle)
                    {
                        this.Rotation -= this.Entity.EntityData.RotateAngleSpeed * gameTime.DeltaTime;
                    }
                    else if (this.Rotation < angle)
                    {
                        this.Rotation += this.Entity.EntityData.RotateAngleSpeed * gameTime.DeltaTime;
                    }
                }
                else
                {
                    this.Rotation = angle;
                }
            }
        }
        private void Move(GameTime gameTime)
        {
            var currentAcceleration = Acceleration;
            var currentVelocity = Velocity;
            currentAcceleration.X = -currentVelocity.X * 0.8f;
            currentAcceleration.Y = -currentVelocity.Y * 0.8f;
            currentVelocity.X += currentAcceleration.X * gameTime.DeltaTime;
            currentVelocity.Y += currentAcceleration.Y * gameTime.DeltaTime;
            //currentVelocity.Y += this.entityData.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            Acceleration = currentAcceleration;
            Velocity = currentVelocity;
        }
        public void Destroy()
        {
            foreach (var ctx in TargetedBy)
            {
                ctx.Key.Target = null;
            }
        }
        private double GetDistanceBetweenPoints(Vector2 firstPoint, Vector2 secondPoint)
        {
            return Math.Sqrt((firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X) + (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y));
        }
        private void SetTargetEnemy(EntityContext nearestEntityContext)
        {
            this.Target = nearestEntityContext;
            this.Target.TargetedBy.Add(this, true);
        }
        private void FireAtTarget(GameTime gameTime)
        {
            if (ReloadTime <= 0.0f)
            {
                ReloadTime = Entity.EntityData.HitSpeed / 1000.0f;
                if (Entity.EntityData.Projectile == null) // Melee
                {
                    Target!.Entity.Hitpoints -= Entity.EntityData.Damage;
                }
                else
                {
                    Arena.Projectiles.Add(new Projectile(Arena, this, Csv.Tables.Get(Csv.Files.Projectiles).GetData<ProjectileData>(Entity.EntityData.Projectile), Target));
                }
            }
            else
            {
                ReloadTime -= gameTime.DeltaTime;
            }
        }
        private void ApproachTarget(GameTime gameTime)
        {

        }
    }
}