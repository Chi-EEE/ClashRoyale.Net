using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using System;
using System.Numerics;
using System.Text.RegularExpressions;

namespace ClashRoyale.Game.Types
{
    public class EntityContext
    {
        public Arena Arena { get; set; }
        public Entity Entity { get; set; }
        public EntityData EntityInformation { get; set; }
        public int Level { get; set; }
        public float DeployTime { get; set; }
        public Entity? Target { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public EntityContext(Arena arena, EntityData entityInformation, Vector2 position, int level = 0)
        {
            Arena = arena;
            Entity = new Entity(EntityInformation, entityInformation.Hitpoints, position);
            EntityInformation = entityInformation;
            Level = level;
            DeployTime = entityInformation.DeployTime;
        }
        //private bool AreCirclesOverlapping(float x1, float y1, float x2, float y2, int r1, int r2)
        //{
        //    return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) <= (r1 + r2) * (r1 + r2);
        //}
        public void Tick(GameTime gameTime)
        {
            var thisPosition = this.Entity.Position;
            //Console.WriteLine(this.EntityInformation.Speed);
            thisPosition.Y += this.EntityInformation.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            //var collisionRadius = this.EntityInformation.CollisionRadius;
            foreach (var entity in Arena.Entities)
            {
                if (entity != this)
                {
                    //Console.WriteLine(AreCirclesOverlapping(this.Position.X, this.Position.Y, entity.Position.X, entity.Position.Y, this.EntityInformation.CollisionRadius, entity.EntityInformation.CollisionRadius));
                    //        var entityPosition = entity.Position;
                    //        if (AreCirclesOverlapping(thisPosition.X, entityPosition.X, thisPosition.Y, entityPosition.Y, collisionRadius, entity.EntityInformation.CollisionRadius))
                    //        {
                    //            float fDistance = MathF.Sqrt((thisPosition.X - entityPosition.X) * (thisPosition.X - entityPosition.X) + (thisPosition.Y - entityPosition.Y) * (thisPosition.Y - entityPosition.Y));

                    //            float fOverlap = 0.5f * (fDistance - collisionRadius - entity.EntityInformation.CollisionRadius);

                    //            thisPosition.X -= fOverlap * (thisPosition.X - entityPosition.X) / fDistance;
                    //            thisPosition.Y -= fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance;

                    //            entityPosition.X += fOverlap * (thisPosition.X - entityPosition.X) / fDistance;
                    //            entityPosition.Y += fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance;

                    //            this.Position = thisPosition;
                    //            entity.Position = entityPosition;
                    //        }
                }
            }
            this.Entity.Position = thisPosition;
        }
    }
}