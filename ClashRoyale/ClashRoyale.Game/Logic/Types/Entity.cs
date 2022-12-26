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
        public Entities EntityInformation { get; set; }
        public int Hitpoints { get; set; }
        public Vector2 Position { get; set; }
        public Entity(Entities entityInformation, int hitpoints, Vector2 position)
        {
            EntityInformation = entityInformation;
            Hitpoints = hitpoints;
            Position = position;
        }
    }
}
