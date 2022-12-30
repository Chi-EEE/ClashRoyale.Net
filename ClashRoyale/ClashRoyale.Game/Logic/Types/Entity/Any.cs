using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public partial class EntityContext
    {
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
                        double distance = GetDistanceBetweenPoints(Entity.Position, entityContext.Entity.Position);
                        if (distance <= Entity.EntityData.Range + Entity.EntityData.CollisionRadius + entityContext.Entity.EntityData.CollisionRadius && distance < nearestDistance)
                        {
                            nearestEntityContext = entityContext;
                            nearestDistance = distance;
                        }
                    }
                }
            }
            if (nearestEntityContext != null)
            {
                SetTargetEnemy(nearestEntityContext);
            }
        }
    }
}
