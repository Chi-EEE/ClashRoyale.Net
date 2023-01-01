using ClashRoyale.Game.GameLoop;

namespace ClashRoyale.Game.Logic.Types
{
    public partial class Projectile
    {
        private void HomingProjectile(GameTime gameTime)
        {
            if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, this.TargetPosition, this.ProjectileData.Radius, Target.Entity.EntityData.CollisionRadius))
            {
                if (this.ProjectileData.Radius > 0)
                {
                    foreach (var entityContext in this.Arena.Entities)
                    {
                        if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, entityContext.Entity.Position, this.ProjectileData.Radius, entityContext.Entity.EntityData.CollisionRadius))
                        {
                            entityContext.Entity.Hitpoints -= this.ProjectileData.Damage;
                        }
                    }
                }
                else
                {
                    Target.Entity.Hitpoints -= this.ProjectileData.Damage;
                }
                Destroyed = true;
            }
        }
    }
}

