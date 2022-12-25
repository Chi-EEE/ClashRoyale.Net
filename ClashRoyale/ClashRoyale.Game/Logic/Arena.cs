using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ClashRoyale.Game.GameLoop;

namespace ClashRoyale.Game
{
    public class Arena
    {
        public const uint REAL_ARENA_WIDTH = 18000;
        public const uint REAL_ARENA_HEIGHT = 32000;
        public List<EntityContext> Entities = new List<EntityContext>();
        private Grid grid = new(36, 64);
        public Arena()
        {
        }
        public void Start()
        {
            Entities.Add(new EntityContext(this, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<Characters>(0), new Vector2(0, 0)));
        }
        private bool AreCirclesOverlapping(float x1, float x2, float y1, float y2, int r1, int r2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) <= (r1 + r2) * (r1 + r2);
        }
        public void Tick(GameTime gameTime)
        {
            foreach (var ctx in Entities)
            {
                ctx.Tick(gameTime);

                //if (entity != this)
                //{
                //    var entityPosition = entity.Position;
                //    if (AreCirclesOverlapping(thisPosition.X, entityPosition.X, thisPosition.Y, entityPosition.Y, collisionRadius, entity.EntityInformation.CollisionRadius))
                //    {
                //        float fDistance = MathF.Sqrt((thisPosition.X - entityPosition.X) * (thisPosition.X - entityPosition.X) + (thisPosition.Y - entityPosition.Y) * (thisPosition.Y - entityPosition.Y));

                //        float fOverlap = 0.5f * (fDistance - collisionRadius - entity.EntityInformation.CollisionRadius);

                //        thisPosition.X -= fOverlap * (thisPosition.X - entityPosition.X) / fDistance;
                //        thisPosition.Y -= fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance;

                //        entityPosition.X += fOverlap * (thisPosition.X - entityPosition.X) / fDistance;
                //        entityPosition.Y += fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance;

                //        this.Position = thisPosition;
                //        entity.Position = entityPosition;
                //    }
                //}
            }
        }
    }
}
