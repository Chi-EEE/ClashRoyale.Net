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

namespace ClashRoyale.Game
{
    public class Arena
    {
        public const uint REAL_ARENA_WIDTH = 18000;
        public const uint REAL_ARENA_HEIGHT = 32000;
        public List<EntityContext> Entities = new List<EntityContext>();
        public Grid Grid = new(36, 64);
        public Arena()
        {
        }
        public void Start()
        {
            //Entities.Add(new EntityContext(this, Csv.Tables.Get(Csv.Files.Characters).GetDataWithInstanceId<EntityData>(0), new Vector2(0, 0)));
        }
        private bool AreCirclesOverlapping(Vector2 firstVector, Vector2 secondVector, int r1, int r2)
        {
            return (firstVector.X - secondVector.X) * (firstVector.X - secondVector.X) + (firstVector.Y - secondVector.Y) * (firstVector.Y - secondVector.Y) <= (r1 + r2) * (r1 + r2);
        }
        public void Tick(GameTime gameTime)
        {
            // Go through all the entities and make them perform their actions
            foreach (var ctx in Entities)
            {
                ctx.Tick(gameTime);
            }
            // Check for collision
            List<Tuple<EntityContext, EntityContext>> collidingPairs = new();
            foreach (var firstEntityCtx in Entities)
            {
                var firstsecondEntityPosition = firstEntityCtx.Entity.Position;
                var firstCollisionRadius = firstEntityCtx.EntityData.CollisionRadius;
                foreach (var secondEntityCtx in this.Entities)
                {
                    if (firstEntityCtx != secondEntityCtx)
                    {
                        var secondEntityPosition = secondEntityCtx.Entity.Position;
                        var secondCollisionRadius = secondEntityCtx.EntityData.CollisionRadius;
                        if (AreCirclesOverlapping(firstsecondEntityPosition, secondEntityPosition, firstCollisionRadius, secondCollisionRadius))
                        {
                            collidingPairs.Add(new(firstEntityCtx, secondEntityCtx));
                            float fDistance = MathF.Sqrt((firstsecondEntityPosition.X - secondEntityPosition.X) * (firstsecondEntityPosition.X - secondEntityPosition.X) + (firstsecondEntityPosition.Y - secondEntityPosition.Y) * (firstsecondEntityPosition.Y - secondEntityPosition.Y));

                            float fOverlap = 0.5f * (fDistance - firstCollisionRadius - secondEntityCtx.EntityData.CollisionRadius);
                            firstsecondEntityPosition.X -= (fOverlap * (firstsecondEntityPosition.X - secondEntityPosition.X) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
                            firstsecondEntityPosition.Y -= (fOverlap * (firstsecondEntityPosition.Y - secondEntityPosition.Y) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;

                            secondEntityPosition.X += (fOverlap * (firstsecondEntityPosition.X - secondEntityPosition.X) / fDistance);// * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
                            secondEntityPosition.Y += (fOverlap * (firstsecondEntityPosition.Y - secondEntityPosition.Y) / fDistance); //* ENTITY_MOVE_SCALE * gameTime.DeltaTime;

                            firstEntityCtx.Entity.Position = firstsecondEntityPosition;
                            secondEntityCtx.Entity.Position = secondEntityPosition;
                        }
                    }
                }
            }
            foreach (var collidingPair in collidingPairs)
            {
                var firstEntity = collidingPair.Item1;
                var secondEntity = collidingPair.Item2;

                var firstPosition = firstEntity.Entity.Position;
                var secondPosition = secondEntity.Entity.Position;

                var firstVelocity = firstEntity.Velocity;
                var secondVelocity = secondEntity.Velocity;

                var firstMass = firstEntity.EntityData.Mass;
                var secondMass = secondEntity.EntityData.Mass;
                float fDistance = MathF.Sqrt((firstPosition.X - secondPosition.X) * (firstPosition.X - secondPosition.X) + (firstPosition.Y - secondPosition.Y) * (firstPosition.Y - secondPosition.Y));

                float nx = (secondPosition.X - firstPosition.X) / fDistance;
                float ny = (secondPosition.Y - firstPosition.Y) / fDistance;

                float tx = -ny;
                float ty = nx;

                float dpTan1 = firstVelocity.X * tx + firstVelocity.Y * ty;
                float dpTan2 = secondVelocity.X * tx + secondVelocity.Y * ty;

                float dpNorm1 = firstVelocity.X * nx + firstVelocity.Y * ny;
                float dpNorm2 = secondVelocity.X * nx + secondVelocity.Y * ny;

                float m1 = (dpNorm1 * (firstMass - secondMass) + 2.0f * secondMass * dpNorm2) / (firstMass + secondMass);
                float m2 = (dpNorm1 * (secondMass - firstMass) + 2.0f * firstMass * dpNorm1) / (firstMass + secondMass);

                firstVelocity.X = tx * dpTan1 + nx * m1;
                firstVelocity.Y = ty * dpTan1 + ny * m1;
                secondVelocity.X = tx * dpTan2 + nx * m2;
                secondVelocity.Y = ty * dpTan2 + ny * m2;

                firstEntity.Velocity = firstVelocity;
                secondEntity.Velocity = secondVelocity;
            }
            foreach (var ctx in Entities)
            {
                ctx.Entity.Position += ctx.Velocity;
            }
        }
    }
}
