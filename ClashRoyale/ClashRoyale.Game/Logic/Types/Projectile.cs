using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types
{
    public class Projectile
    {
        private Arena Arena { get; set; }
        public ProjectileData ProjectileData { get; set; }
        private Vector2 SpawnPosition { get; set; }
        public Vector2 CurrentPosition { get; set; }
        private Vector2 PreviousPosition { get; set; }
        private double DistanceTravelled { get; set; }
        private EntityContext Target { get; set; }
        private Vector2 TargetPosition { get; set; }
        private Vector2 DirectionVector { get; set; }
        private Dictionary<EntityContext, bool> AlreadyHitEntities { get; set; }
        public bool Destroyed { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public Projectile(Arena arena, Vector2 spawnPosition, ProjectileData projectileData, EntityContext target)
        {
            Arena = arena;
            ProjectileData = projectileData;
            SpawnPosition = spawnPosition;
            CurrentPosition = spawnPosition;
            DistanceTravelled = 0;
            Target = target;
            if (this.ProjectileData.ProjectileRange > 0)
            {
                TargetPosition = Vector2.Normalize(CurrentPosition) * this.ProjectileData.ProjectileRange;
            }
            else
            {
                TargetPosition = target.Entity.Position;
            }
            DirectionVector = Vector2.Normalize(TargetPosition - CurrentPosition);
            AlreadyHitEntities = new();
            if (this.ProjectileData.Homing)
            {
                target.EstimatedHitpoints -= this.ProjectileData.Damage;
                Console.WriteLine("Estimated Health: " + target.EstimatedHitpoints);
            }
        }
        private double GetDistanceBetweenTwoPoints(Vector2 point1, Vector2 point2)
        {
            return Math.Pow(point1.Y - point2.Y, 2) + Math.Pow(point1.X - point2.X, 2);
        }
        private double GetTwoRadiusSize(int radius1, int radius2)
        {
            return (radius1 + radius2) * (radius1 + radius2);
        }
        private bool CheckDistanceFromProjectileAndEntity(Vector2 currentPosition, Vector2 entityPosition, int radius, int entityRadius)
        {
            return GetDistanceBetweenTwoPoints(currentPosition, entityPosition) <= GetTwoRadiusSize(radius, entityRadius);
        }
        public void Tick(GameTime gameTime)
        {
            if (this.ProjectileData.Homing)
            {
                if (Target.Entity.Position != TargetPosition)
                {
                    this.TargetPosition = Target.Entity.Position;
                    this.DirectionVector = Vector2.Normalize(CurrentPosition - TargetPosition);
                }
            }
            DistanceTravelled += Math.Sqrt(GetDistanceBetweenTwoPoints(this.CurrentPosition, this.PreviousPosition));
            Move(gameTime);
            // OnlyEnemies is not used

            CheckAndDamageEntities(gameTime);
        }
        private void Move(GameTime gameTime)
        {
            this.PreviousPosition = this.CurrentPosition;
            this.CurrentPosition += this.DirectionVector * this.ProjectileData.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            Console.WriteLine(this.CurrentPosition);
        }
        private void HitEntity(GameTime gameTime, EntityContext entityContext)
        {
            entityContext.Entity.Hitpoints -= this.ProjectileData.Damage;
            entityContext.Velocity += Vector2.Normalize(this.CurrentPosition - entityContext.Entity.Position) * this.ProjectileData.Pushback;
        }
        private void CheckAndDamageEntities(GameTime gameTime)
        {
            // Is far enough to be valid
            if (DistanceTravelled >= this.ProjectileData.MinDistance) // MaxDistance is not used
            {
                if (this.ProjectileData.Homing)
                {
                    // At the target's position
                    if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, this.TargetPosition, this.ProjectileData.Radius, Target.Entity.EntityData.CollisionRadius))
                    {
                        // To check if crown tower
                        //Target.Entity.EntityData.CrownTowerDamagePercent
                        Target.Entity.Hitpoints -= this.ProjectileData.Damage;
                        Console.WriteLine("Health: " + Target.Entity.Hitpoints);
                        Destroyed = true;
                    }
                }
                else if (GetDistanceBetweenTwoPoints(this.CurrentPosition, this.TargetPosition) == 0)
                {
                    Destroyed = true;
                    // If at the target position
                    if (this.ProjectileData.AoeToAir ||
                        this.ProjectileData.AoeToGround)
                    {
                        // Can probably store a function and call that function instead of checking every tick
                        if (this.ProjectileData.AoeToAir &&
                            this.ProjectileData.AoeToGround)
                        {
                            foreach (EntityContext entityContext in this.Arena.Entities)
                            {
                                // At the target's position
                                if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                                {
                                    HitEntity(gameTime, entityContext);
                                }
                            }
                        }
                        else if (this.ProjectileData.AoeToAir)
                        {
                            foreach (EntityContext entityContext in this.Arena.Entities)
                            {
                                if (entityContext.Entity.EntityData.FlyingHeight > 0)
                                {
                                    if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                                    {
                                        HitEntity(gameTime, entityContext);
                                    }
                                }
                            }
                        }
                        else if (this.ProjectileData.AoeToGround)
                        {
                            foreach (EntityContext entityContext in this.Arena.Entities)
                            {
                                if (entityContext.Entity.EntityData.FlyingHeight == 0)
                                {
                                    if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                                    {
                                        HitEntity(gameTime, entityContext);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("HOW? : " + ProjectileData.ToString());
                    }
                }
                // This will always happen if ProjectileRadius is greater than 0 (Like log or barb barrel)
                if (this.ProjectileData.ProjectileRadius > 0)
                {
                    // Can probably store a function and call that function instead of checking every tick
                    DamageIndirectNearbyEntities(gameTime);
                }
            }
        }
        private void DamageIndirectNearbyEntities(GameTime gameTime)
        {
            if (this.ProjectileData.AoeToAir &&
                        this.ProjectileData.AoeToGround)
            {
                foreach (EntityContext entityContext in this.Arena.Entities)
                {
                    if (!this.AlreadyHitEntities.ContainsKey(entityContext) && CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.ProjectileRadius, entityContext.Entity.EntityData.CollisionRadius))
                    {
                        this.AlreadyHitEntities.Add(entityContext, true);
                        HitEntity(gameTime, entityContext);
                    }
                }
            }
            else if (this.ProjectileData.AoeToAir)
            {
                foreach (EntityContext entityContext in this.Arena.Entities)
                {
                    if (entityContext.Entity.EntityData.FlyingHeight > 0)
                    {
                        if (!this.AlreadyHitEntities.ContainsKey(entityContext) && CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.ProjectileRadius, entityContext.Entity.EntityData.CollisionRadius))
                        {
                            this.AlreadyHitEntities.Add(entityContext, true);
                            HitEntity(gameTime, entityContext);
                        }
                    }
                }
            }
            else if (this.ProjectileData.AoeToGround)
            {
                foreach (EntityContext entityContext in this.Arena.Entities)
                {
                    if (entityContext.Entity.EntityData.FlyingHeight == 0)
                    {
                        if (!this.AlreadyHitEntities.ContainsKey(entityContext) && CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.ProjectileRadius, entityContext.Entity.EntityData.CollisionRadius))
                        {
                            this.AlreadyHitEntities.Add(entityContext, true);
                            HitEntity(gameTime, entityContext);
                        }
                    }
                }
            }
        }
    }
}

