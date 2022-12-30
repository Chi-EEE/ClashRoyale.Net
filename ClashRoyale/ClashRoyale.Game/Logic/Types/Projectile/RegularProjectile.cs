using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic.Types.Entity;

namespace ClashRoyale.Game.Logic.Types
{
    public partial class Projectile
    {
        private void AOE_Everything(GameTime gameTime)
        {
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                // At the target's position
                if (!this.AlreadyHitEntities.ContainsKey(entityContext) && CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                {
                    HitEntity(gameTime, entityContext);
                }
            }
        }
        private void AOE_Air(GameTime gameTime)
        {
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                if (!this.AlreadyHitEntities.ContainsKey(entityContext) && entityContext.Entity.IsAir())
                {
                    if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                    {
                        HitEntity(gameTime, entityContext);
                    }
                }
            }
        }
        private void AOE_Ground(GameTime gameTime)
        {
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                if (!this.AlreadyHitEntities.ContainsKey(entityContext) && entityContext.Entity.IsGround())
                {
                    if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                    {
                        HitEntity(gameTime, entityContext);
                    }
                }
            }
        }
        private void RegularProjectile(GameTime gameTime)
        {
            if (GetDistanceBetweenTwoPoints(this.CurrentPosition, this.TargetPosition) == 0)
            {
                Destroyed = true;
                NotHomingAoeFunction.DynamicInvoke();
            }
        }
    }
}

