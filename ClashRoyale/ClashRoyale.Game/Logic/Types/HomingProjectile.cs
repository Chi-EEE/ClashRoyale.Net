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
    public partial class Projectile
    {
        private void HomingProjectile(GameTime gameTime)
        {
            // At the target's position
            if (CheckDistanceFromProjectileAndEntity(this.CurrentPosition, this.TargetPosition, this.ProjectileData.Radius, Target.Entity.EntityData.CollisionRadius))
            {
                // To check if crown tower
                //Target.Entity.EntityData.CrownTowerDamagePercent
                Target.Entity.Hitpoints -= this.ProjectileData.Damage;
                Destroyed = true;
            }
        }
    }
}

