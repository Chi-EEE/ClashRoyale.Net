using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using ClashRoyale.Game.Logic;
using ClashRoyale.Game.Logic.Types;
using System;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public partial class EntityContext
    {
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
