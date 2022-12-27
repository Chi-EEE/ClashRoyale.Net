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
        public EntityData EntityData { get; set; }
        public int Level { get; set; }
        public float DeployTime { get; set; }
        public Vector2 Velocity { get; set; }
        public Entity? Target { get; set; }
        private const float ENTITY_MOVE_SCALE = 21.333333333333333333333333333333f;
        public EntityContext(Arena arena, EntityData entityInformation, Vector2 position, int level = 0)
        {
            Arena = arena;
            Entity = new Entity(entityInformation, entityInformation.Hitpoints, position);
            EntityData = entityInformation;
            Level = level;
            DeployTime = entityInformation.DeployTime;
            Velocity = new(0, 0);
        }
        public void Tick(GameTime gameTime)
        {
            Move(gameTime);
        }
        private void Move(GameTime gameTime)
        {
            var currentVelocity = this.Velocity;
            //currentVelocity.Y += this.EntityInformation.Speed * ENTITY_MOVE_SCALE * gameTime.DeltaTime;
            this.Velocity = currentVelocity;
        }
    }
}