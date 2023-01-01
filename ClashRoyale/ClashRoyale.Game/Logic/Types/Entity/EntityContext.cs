using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic;
using ClashRoyale.Game.Logic.Types;
using SFML.System;
using System;
using System.Numerics;
using System.Reflection.Metadata;
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
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public float EstimatedHitpoints { get; set; }
        public Dictionary<EntityContext, bool> TargetedBy { get; set; }
        public EntityContext? Target { get; set; }
        public Vector2? TargetPosition { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        private Vector2 MovementDirection { get; set; }
        private Action GetNearestEnemy { get; set; }
        private List<Action<GameTime>> PassiveAbilities { get; set; }


        public float SpeedMultipler { get; set; }
        public int Damage { get; set; }
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
            SpeedMultipler = 1.0f;
            Damage = entity.EntityData.Damage;
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
            SetupCharge();
        }
        private void SetupCharge()
        {
            if (this.Entity.EntityData.ChargeRange > 0)
            {
                PassiveAbilities.Add();
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
                        Move(gameTime);
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
                    var δ = (angle - this.Rotation + 540) % 360 - 180;
                    if (δ > 0)
                    {
                        this.Rotation = (this.Rotation + this.Entity.EntityData.RotateAngleSpeed * gameTime.DeltaTime) % 360;
                    }
                    else if (δ < 0)
                    {
                        this.Rotation = (this.Rotation - this.Entity.EntityData.RotateAngleSpeed * gameTime.DeltaTime) % 360;
                    }
                    if (Math.Abs(angle - this.Rotation) % 360 <= 15)
                    {
                        this.Rotation = angle;
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
            //var currentAcceleration = Acceleration;
            //var currentVelocity = this.Velocity;
            //currentAcceleration.X = -currentVelocity.X * 0.8f;
            //currentAcceleration.Y = -currentVelocity.Y * 0.8f;
            //currentVelocity.X += currentAcceleration.X * gameTime.DeltaTime;
            //currentVelocity.Y += currentAcceleration.Y * gameTime.DeltaTime;
            ////currentVelocity.Y += this.entityData.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            //Acceleration = currentAcceleration;
            //this.Velocity = currentVelocity;
            this.Entity.Position += this.MovementDirection * this.Entity.EntityData.Speed * this.SpeedMultipler * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
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
        private void FireAtTarget(GameTime gameTime)
        {
            if (this.ReloadTime <= 0.0f)
            {
                this.ReloadTime = this.Entity.EntityData.HitSpeed / 1000.0f;
                if (this.Entity.EntityData.Projectile == null) // Melee
                {
                    this.Target!.TakeMeleeDamage(this.Damage);
                }
                else
                {
                    this.Arena.Projectiles.Add(new Projectile(this.Arena, this, Csv.Tables.Get(Csv.Files.Projectiles).GetData<ProjectileData>(this.Entity.EntityData.Projectile), Target));
                }
            }
            else
            {
                ReloadTime -= gameTime.DeltaTime;
            }
        }
        public void TakeMeleeDamage(int damage)
        {
            this.EstimatedHitpoints -= damage;
            this.Entity.Hitpoints -= damage;
        }
    }
}