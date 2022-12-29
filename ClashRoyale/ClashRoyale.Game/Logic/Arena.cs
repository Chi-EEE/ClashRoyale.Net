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
using ClashRoyale.Game.Logic.Pathing;
using ClashRoyale.Game.Files.CsvTilemap;
using SFML.System;

namespace ClashRoyale.Game
{
    public class Arena
    {
        public const uint REAL_ARENA_WIDTH = 18000;
        public const uint REAL_ARENA_HEIGHT = 32000;
        public List<EntityContext> Entities = new List<EntityContext>();
        public Tilemap Tilemap = new("GameAssets/tilemaps/tilemap.csv");
        public Arena()
        {
            foreach (Entity building in Tilemap.Buildings)
            {
                Entities.Add(new EntityContext(this, building));
            }
        }
        public void Start()
        {
        }
        private float GetDistanceBetweenPoints(Vector2 firstPoint, Vector2 secondPoint)
        {
            return (firstPoint.X - secondPoint.X) * (firstPoint.X - secondPoint.X) + (firstPoint.Y - secondPoint.Y) * (firstPoint.Y - secondPoint.Y);
        }
        private Vector2 GetInelasticCollisionVelocity(Vector2 firstVelocity, Vector2 secondVelocity, float firstMass, float secondMass)
        {
            return (firstMass * firstVelocity + secondMass * secondVelocity) / (firstMass + secondMass);
        }
        private void GetElasticCollisionVelocity(Vector2 firstPosition, Vector2 secondPosition, Vector2 firstVelocity, Vector2 secondVelocity, float firstMass, float secondMass, out Vector2 firstResultVelocity, out Vector2 secondResultVelocity)
        {
            float fDistance = MathF.Sqrt((firstPosition.X - secondPosition.X) * (firstPosition.X - secondPosition.X) + (firstPosition.Y - secondPosition.Y) * (firstPosition.Y - secondPosition.Y));

            float nx = (secondPosition.X - firstPosition.X) / fDistance;
            float ny = (secondPosition.Y - firstPosition.Y) / fDistance;

            float tx = -ny;
            float ty = nx;
            float dpNorm1 = firstVelocity.X * nx + firstVelocity.Y * ny;
            float dpNorm2 = secondVelocity.X * nx + secondVelocity.Y * ny;

            if (firstMass > 0)
            {
                float m1 = (dpNorm1 * (firstMass - secondMass) + 2.0f * secondMass * dpNorm2) / (firstMass + secondMass);
                float dpTan1 = firstVelocity.X * tx + firstVelocity.Y * ty;
                firstVelocity.X = tx * dpTan1 + nx * m1;
                firstVelocity.Y = ty * dpTan1 + ny * m1;
                firstResultVelocity = firstVelocity;
            } else
            {
                firstResultVelocity = new(0, 0);
            }
            if (secondMass > 0)
            {
                float m2 = (dpNorm2 * (secondMass - firstMass) + 2.0f * firstMass * dpNorm1) / (firstMass + secondMass);
                float dpTan2 = secondVelocity.X * tx + secondVelocity.Y * ty;
                secondVelocity.X = tx * dpTan2 + nx * m2;
                secondVelocity.Y = ty * dpTan2 + ny * m2;
                secondResultVelocity = secondVelocity;
            }
            else
            {
                secondResultVelocity = new(0, 0);
            }
        }
        public void Tick(GameTime gameTime)
        {
            // Go through all the entities and make them perform their actions
            foreach (var ctx in Entities)
            {
                if (ctx.Entity.Hitpoints > 0)
                {
                    ctx.Tick(gameTime);
                } else
                {
                    Entities.Remove(ctx);
                }
            }
            // Check for collision
            List<Tuple<EntityContext, EntityContext>> collidingPairs = new();
            foreach (var firstEntityContext in Entities)
            {
                var firstEntityPosition = firstEntityContext.Entity.Position;
                var firstCollisionRadius = firstEntityContext.Entity.EntityData.CollisionRadius;
                foreach (var secondEntityContext in this.Entities)
                {
                    if (firstEntityContext != secondEntityContext)
                    {
                        var secondEntityPosition = secondEntityContext.Entity.Position;
                        var secondCollisionRadius = secondEntityContext.Entity.EntityData.CollisionRadius;

                        float distance = GetDistanceBetweenPoints(firstEntityPosition, secondEntityPosition);
                        if (distance <= (firstCollisionRadius + secondCollisionRadius) *(firstCollisionRadius + secondCollisionRadius))
                        {
                            collidingPairs.Add(new(firstEntityContext, secondEntityContext));

                            Vector2 directionalVector;
                            if (distance == 0)
                            {
                                directionalVector = Vector2.UnitY;
                            } else
                            {
                                float fDistance = MathF.Sqrt((firstEntityPosition.X - secondEntityPosition.X) * (firstEntityPosition.X - secondEntityPosition.X) + (firstEntityPosition.Y - secondEntityPosition.Y) * (firstEntityPosition.Y - secondEntityPosition.Y));
                                directionalVector = (firstEntityPosition - secondEntityPosition) / fDistance;
                            }
                            if (firstEntityContext.Entity.EntityData.Mass > 0)
                            {
                                firstEntityPosition.X += directionalVector.X * 1000 * gameTime.DeltaTime;
                                firstEntityPosition.Y += directionalVector.Y * 1000 * gameTime.DeltaTime;
                                firstEntityContext.Entity.Position = firstEntityPosition;
                            }
                            if (secondEntityContext.Entity.EntityData.Mass > 0)
                            {
                                secondEntityPosition.X -= directionalVector.X * 1000 * gameTime.DeltaTime;
                                secondEntityPosition.Y -= directionalVector.Y * 1000 * gameTime.DeltaTime;
                                secondEntityContext.Entity.Position = secondEntityPosition;
                            }
                        }
                    }
                }
            }
            foreach (var collidingPair in collidingPairs)
            {
                var firstEntityContext = collidingPair.Item1;
                var secondEntityContext = collidingPair.Item2;

                //var firstPosition = firstEntity.Entity.Position;
                //var secondPosition = secondEntity.Entity.Position;

                var firstVelocity = firstEntityContext.Velocity;
                var secondVelocity = secondEntityContext.Velocity;

                var firstMass = firstEntityContext.Entity.EntityData.Mass;
                var secondMass = secondEntityContext.Entity.EntityData.Mass;

                Vector2 finalVelocity = GetInelasticCollisionVelocity(firstVelocity, secondVelocity, firstMass, secondMass);

                if (firstEntityContext.Entity.EntityData.Mass > 0)
                {
                    firstEntityContext.Velocity = finalVelocity;
                }
                if (secondEntityContext.Entity.EntityData.Mass > 0)
                {
                    secondEntityContext.Velocity = finalVelocity;
                }
            }
            foreach (var ctx in Entities)
            {
                ctx.Entity.Position += ctx.Velocity;
            }
        }
    }
}
