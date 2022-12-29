using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.GameLoop;
using System;
using System.Numerics;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;

namespace ClashRoyale.Game.Types
{
    public class EntityContext
    {
        public Arena Arena { get; set; }
        public Entity Entity { get; set; }
        public int Level { get; set; }
        public float DeployTime { get; set; }
        public Vector2 Velocity { get; set; }
        public Vector2 Acceleration { get; set; }
        public Entity? Target { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public EntityContext(Arena arena, EntityData entityInformation, Vector2 position, int level = 0)
        {
            Arena = arena;
            Entity = new Entity(entityInformation, entityInformation.Hitpoints, position);
            Level = level;
            DeployTime = entityInformation.DeployTime;
            Velocity = new(0, 0);
        }
        public EntityContext(Arena arena, Entity entity)
        {
            Arena = arena;
            Entity = entity;
            DeployTime = 0;
            Velocity = new(0, 0);
        }
        public void Tick(GameTime gameTime)
        {
            Move(gameTime);
        }
        private void Move(GameTime gameTime)
        {
            var currentAcceleration = this.Acceleration;
            var currentVelocity = this.Velocity;
            currentAcceleration.X = -currentVelocity.X * 0.8f;
            currentAcceleration.Y = -currentVelocity.Y * 0.8f;
            currentVelocity.X += currentAcceleration.X * gameTime.DeltaTime;
            currentVelocity.Y += currentAcceleration.Y * gameTime.DeltaTime;
            //currentVelocity.Y += this.EntityInformation.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            this.Acceleration = currentAcceleration;
            this.Velocity = currentVelocity;
        }
    }
}