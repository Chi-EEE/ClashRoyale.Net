using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.Logic;
using System.Numerics;

namespace ClashRoyale.Game.Types
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
    }
}
