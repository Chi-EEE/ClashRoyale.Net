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
            Entity = new Entity(entityInformation, entityInformation.Hitpoints, position);
            EntityInformation = entityInformation;
            Level = level;
            DeployTime = entityInformation.DeployTime;
        }
        private bool AreCirclesOverlapping(float x1, float y1, float x2, float y2, int r1, int r2)
        {
            return (x1 - x2) * (x1 - x2) + (y1 - y2) * (y1 - y2) <= (r1 + r2) * (r1 + r2);
        }
        public void Tick(GameTime gameTime)
        {
            var thisPosition = this.Entity.Position;
            //Console.WriteLine(this.EntityInformation.Speed);
            //thisPosition.Y += this.EntityInformation.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            var collisionRadius = this.EntityInformation.CollisionRadius;
            foreach (var entityCtx in Arena.Entities)
            {
                if (entityCtx != this)
                {
                    var entityPosition = entityCtx.Entity.Position;
                    if (AreCirclesOverlapping(thisPosition.X, thisPosition.Y, entityPosition.X,  entityPosition.Y, collisionRadius, entityCtx.EntityInformation.CollisionRadius))
                    {
                        float fDistance = MathF.Sqrt((thisPosition.X - entityPosition.X) * (thisPosition.X - entityPosition.X) + (thisPosition.Y - entityPosition.Y) * (thisPosition.Y - entityPosition.Y));

                        float fOverlap = 0.5f * (fDistance - collisionRadius - entityCtx.EntityInformation.CollisionRadius);
                        thisPosition.X -= (fOverlap * (thisPosition.X - entityPosition.X) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
                        thisPosition.Y -= (fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;

                        entityPosition.X += (fOverlap * (thisPosition.X - entityPosition.X) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
                        entityPosition.Y += (fOverlap * (thisPosition.Y - entityPosition.Y) / fDistance); //* ENTITY_MOVE_SCALE * gameTime.DeltaTime;

                        this.Entity.Position = thisPosition;
                        entityCtx.Entity.Position = entityPosition;
                    }
                }
            }
            this.Entity.Position = thisPosition;
        }
    }
}