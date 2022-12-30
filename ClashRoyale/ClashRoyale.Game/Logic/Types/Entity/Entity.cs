using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.Logic;
using System.Numerics;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public class Entity
    {
        public EntityData EntityData { get; set; }
        public int Hitpoints { get; set; }
        public Vector2 Position { get; set; }
        public Team Team { get; set; }
        public Entity(EntityData entityData, int hitpoints, Vector2 position, Team team)
        {
            EntityData = entityData;
            Hitpoints = hitpoints;
            Position = position;
            Team = team;
        }
        public bool IsGround()
        {
            return this.EntityData.FlyingHeight == 0;
        }
        public bool IsAir()
        {
            return this.EntityData.FlyingHeight > 0;
        }
    }
}
