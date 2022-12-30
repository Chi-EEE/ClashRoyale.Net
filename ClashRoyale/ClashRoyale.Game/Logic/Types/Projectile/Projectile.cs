using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic.Types.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types
{
    public partial class Projectile
    {
        public ProjectileData ProjectileData { get; set; }
        private Arena Arena { get; set; }
        private EntityContext EntityFired { get; set; }
        public Vector2 CurrentPosition { get; set; }
        private Vector2 PreviousPosition { get; set; }
        private double DistanceTravelled { get; set; }
        private EntityContext Target { get; set; }
        private Vector2 TargetPosition { get; set; }
        private Vector2 DirectionVector { get; set; }
        private Dictionary<EntityContext, bool> AlreadyHitEntities { get; set; }
        public bool Destroyed { get; set; }
        private Action<GameTime> ReachedTargetPositionFunction;
        private Action<GameTime> NotHomingAoeFunction;
        private Action<GameTime> DamageIndirectNearbyEntitiesFunction;
        

        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public Projectile(Arena arena, EntityContext entityFired, ProjectileData projectileData, EntityContext target)
        {
            Arena = arena;
            ProjectileData = projectileData;
            EntityFired = entityFired;
            CurrentPosition = entityFired.Entity.Position;
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
                ReachedTargetPositionFunction = HomingProjectile;
                target.EstimatedHitpoints -= this.ProjectileData.Damage;
            }
            else
            {
                ReachedTargetPositionFunction = RegularProjectile;
                if (this.ProjectileData.AoeToAir &&
                            this.ProjectileData.AoeToGround)
                {
                    NotHomingAoeFunction = AOE_Everything;
                    DamageIndirectNearbyEntitiesFunction = Indirect_AOE_Everything;
                }
                else if (this.ProjectileData.AoeToAir)
                {
                    NotHomingAoeFunction = AOE_Air;
                    DamageIndirectNearbyEntitiesFunction = Indirect_AOE_Air;
                }
                else if (this.ProjectileData.AoeToGround)
                {
                    NotHomingAoeFunction = AOE_Ground;
                    DamageIndirectNearbyEntitiesFunction = Indirect_AOE_Ground;
                }
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
        }
        private void HitEntity(GameTime gameTime, EntityContext entityContext)
        {
            entityContext.Entity.Hitpoints -= this.ProjectileData.Damage;
            entityContext.Velocity += Vector2.Normalize(this.CurrentPosition - entityContext.Entity.Position) * this.ProjectileData.Pushback;
        }
        private void CheckAndDamageEntities(GameTime gameTime)
        {
            // Is far enough to be valid
            if (GetDistanceBetweenTwoPoints(this.CurrentPosition, this.EntityFired.Entity.Position) >= this.ProjectileData.MinDistance) // MaxDistance is not used
            {
                ReachedTargetPositionFunction.DynamicInvoke(gameTime);
                if (this.ProjectileData.ProjectileRadius > 0)
                {
                    DamageIndirectNearbyEntitiesFunction.DynamicInvoke(gameTime);
                }
            }
        }
    }
}

