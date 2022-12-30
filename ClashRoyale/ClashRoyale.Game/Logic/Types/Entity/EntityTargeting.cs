using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public partial class EntityContext
    {
        private double ClosestDistance { get; set; }
        private float GetCombinedSightRangeRadius(EntityContext entityContext)
        {
            return this.Entity.EntityData.SightRange + this.Entity.EntityData.CollisionRadius + entityContext.Entity.EntityData.CollisionRadius;
        }
        private void CheckCloseEnough(ref EntityContext? nearestEntityContext, ref double nearestDistance, EntityContext entityContext)
        {
            double distance = GetDistanceBetweenPoints(this.Entity.Position, entityContext.Entity.Position);
            bool inSightRange = distance <= GetCombinedSightRangeRadius(entityContext);
            if (inSightRange && distance < nearestDistance)
            {
                nearestEntityContext = entityContext;
                nearestDistance = distance;
            }
        }
        private void GetNearestAnyEnemy()
        {
            EntityContext? nearestEntityContext = null;
            double nearestDistance = double.MaxValue;
            foreach (EntityContext entityContext in Arena.Entities)
            {
                if (entityContext.Entity.Team != Entity.Team)
                {
                    if (entityContext.EstimatedHitpoints > 0)
                    {
                        CheckCloseEnough(ref nearestEntityContext, ref nearestDistance, entityContext);
                    }
                }
            }
            if (nearestEntityContext != null)
            {
                SetTargetEnemy(nearestEntityContext);
                ClosestDistance = nearestDistance;
            }
        }
        private void GetNearestGroundEnemy()
        {
            EntityContext? nearestEntityContext = null;
            double nearestDistance = double.MaxValue;
            foreach (EntityContext entityContext in Arena.Entities)
            {
                if (entityContext.Entity.IsGround() && entityContext.Entity.Team != Entity.Team)
                {
                    if (entityContext.EstimatedHitpoints > 0)
                    {
                        CheckCloseEnough(ref nearestEntityContext, ref nearestDistance, entityContext);
                    }
                }
            }
            if (nearestEntityContext != null)
            {
                SetTargetEnemy(nearestEntityContext);
                ClosestDistance = nearestDistance;
            }
        }
        private void GetNearestAirEnemy()
        {
            EntityContext? nearestEntityContext = null;
            double nearestDistance = double.MaxValue;
            foreach (EntityContext entityContext in Arena.Entities)
            {
                if (entityContext.Entity.IsAir() && entityContext.Entity.Team != Entity.Team)
                {
                    if (entityContext.EstimatedHitpoints > 0)
                    {
                        CheckCloseEnough(ref nearestEntityContext, ref nearestDistance, entityContext);
                    }
                }
            }
            if (nearestEntityContext != null)
            {
                SetTargetEnemy(nearestEntityContext);
                ClosestDistance = nearestDistance;
            }
        }
    }
}
