using ClashRoyale.Files.CsvLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Types
{
    public class Entity
    {
        public EntityData EntityData { get; set; }
        public int Hitpoints { get; set; }
        public Vector2 Position { get; set; }
        public Entity(EntityData entityData, int hitpoints, Vector2 position)
        {
            EntityData = entityData;
            Hitpoints = hitpoints;
            Position = position;
        }
    }
}
