using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic.Types.Entity;

namespace ClashRoyale.Game.Logic.Types
{
    public partial class Projectile
    {
        private void  Indirect_AOE_Everything(GameTime gameTime)
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
        private void Indirect_AOE_Air(GameTime gameTime)
        {
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                if (entityContext.Entity.IsAir())
                {
                    if (!this.AlreadyHitEntities.ContainsKey(entityContext) && CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.ProjectileRadius, entityContext.Entity.EntityData.CollisionRadius))
                    {
                        this.AlreadyHitEntities.Add(entityContext, true);
                        HitEntity(gameTime, entityContext);
                    }
                }
            }
        }
        private void Indirect_AOE_Ground(GameTime gameTime)
        {
            foreach (EntityContext entityContext in this.Arena.Entities)
            {
                if (entityContext.Entity.IsGround())
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