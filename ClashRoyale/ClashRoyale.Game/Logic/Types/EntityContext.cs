using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic;
using ClashRoyale.Game.Logic.Types;
using System;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace ClashRoyale.Game.Types
{
    public class EntityContext
    {
        public Arena Arena { get; set; }
        public Entity Entity { get; set; }
        public int Level { get; set; }
        public float DeployTime { get; set; }
        public float ReloadTime { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float EstimatedHitpoints { get; set; }
        public Dictionary<EntityContext, bool> TargetedBy { get; set; }
        public EntityContext? Target { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public EntityContext(Arena arena, EntityData entityData, Vector2 position, int level = 0)
        {
            Arena = arena;
            Entity = new Entity(entityData, entityData.Hitpoints, position, Team.Blue);
            Level = level;
            ReloadTime = 0;
            DeployTime = entityData.DeployTime;
            Velocity = new(0, 0);
            EstimatedHitpoints = entityData.Hitpoints;
            TargetedBy = new();
        }
        public EntityContext(Arena arena, Entity entity)
        {
            Arena = arena;
            Entity = entity;
            ReloadTime = 0;
            DeployTime = 0;
            Velocity = new(0, 0);
            EstimatedHitpoints = entity.EntityData.Hitpoints;
            TargetedBy = new();
        }
        public void Tick(GameTime gameTime)
        {
            Move(gameTime);
            if (this.Target != null)
            {
                if (this.Target.EstimatedHitpoints <= 0)
                {
                    this.Target.TargetedBy.Remove(this);
                    this.Target = null;
                }
                else
                {
                    double distance = GetDistanceBetweenPoints(this.Entity.Position, this.Target.Entity.Position);
                    if (distance > this.Entity.EntityData.Range + this.Entity.EntityData.CollisionRadius)
                    {
                        this.Target.TargetedBy.Remove(this);
                        this.Target = null;
                    }
                }
            }
            if (this.Target == null)
            {
                GetNearestEnemy();
            }
            if (this.Target != null)
            {
                FireAtTarget(gameTime);
            }
        }
        private void Move(GameTime gameTime)
        {
            var currentAcceleration = this.Acceleration;
            var currentVelocity = this.Velocity;
            currentAcceleration.X = -currentVelocity.X * 0.8f;
            currentAcceleration.Y = -currentVelocity.Y * 0.8f;
            currentVelocity.X += currentAcceleration.X * gameTime.DeltaTime;
            currentVelocity.Y += currentAcceleration.Y * gameTime.DeltaTime;
            //currentVelocity.Y += this.entityData.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            this.Acceleration = currentAcceleration;
            this.Velocity = currentVelocity;
        }
        public void Destroy()
        {
            foreach (var ctx in this.TargetedBy)
            {
                ctx.Key.Target = null;
            }
        }
        private double GetDistanceBetweenPoints(Vector2 firstPoint, Vector2 secondPoint)
        {
            return Math.Sqrt((firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X) + (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y));
        }
        private void GetNearestEnemy()
        {
            EntityContext? nearestEntityContext = null;
            double nearestDistance = double.MaxValue;
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                if (entityContext.Entity.Team != this.Entity.Team)
                {
                    if (entityContext.EstimatedHitpoints > 0)
                    {
                        double distance = GetDistanceBetweenPoints(this.Entity.Position, entityContext.Entity.Position);
                        if (distance <= this.Entity.EntityData.Range + this.Entity.EntityData.CollisionRadius && distance < nearestDistance)
                        {
                            nearestEntityContext = entityContext;
                            nearestDistance = distance;
                        }
                    }
                }
            }
            if (nearestEntityContext != null)
            {
                this.Target = nearestEntityContext;
                this.Target.TargetedBy.Add(this, true);
            }
        }
        private void FireAtTarget(GameTime gameTime)
        {
            this.ReloadTime = this.ReloadTime - gameTime.DeltaTime;
            if (this.ReloadTime <= 0)
            {
                this.ReloadTime = this.Entity.EntityData.HitSpeed / 1000.0f;
                this.Arena.Projectiles.Add(new Projectile(this.Arena, this.Entity.Position, Csv.Tables.Get(Csv.Files.Projectiles).GetData<ProjectileData>(this.Entity.EntityData.Projectile), this.Target));
            }
        }
    }
}