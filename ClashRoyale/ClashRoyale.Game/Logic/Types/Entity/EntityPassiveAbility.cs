using ClashRoyale.Game.GameLoop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public abstract class EntityPassiveAbility
    {
        public abstract void Tick(EntityContext entityContext, GameTime gameTime);
    }
    public class EntityCharge : EntityPassiveAbility
    {
        private double DistanceTravelled { get; set; }
        private Vector2 PreviousPosition { get; set; }
        public EntityCharge(EntityContext entityContext)
        {
            PreviousPosition = entityContext.Entity.Position;
        }
        public override void Tick(EntityContext entityContext, GameTime gameTime)
        {
            if (entityContext.LoadTime <= 0)
            {
                DistanceTravelled += Math.Sqrt(Vector2_Helper.GetDistanceBetweenTwoPoints(entityContext.Entity.Position, this.PreviousPosition));
                PreviousPosition = entityContext.Entity.Position;
                if (DistanceTravelled >= entityContext.Entity.EntityData.ChargeRange)
                {
                    entityContext.SpeedMultipler *= 2.0f;
                    entityContext.Damage = entityContext.Entity.EntityData.DamageSpecial;
                }
            } else
            {
                entityContext.SpeedMultipler *= 2.0f;
                entityContext.Damage = entityContext.Entity.EntityData.DamageSpecial;
            }
        }
    }
}
