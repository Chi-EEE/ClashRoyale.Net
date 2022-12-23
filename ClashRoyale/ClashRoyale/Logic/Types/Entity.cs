using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Simulator.Types
{
    public abstract class Entity
    {
        public Entity(int level, int hitpoints)
        {
            Level = level;
            Hitpoints = hitpoints;
        }
        public int Level { get; set; }
        public int Hitpoints { get; set; }
    }
}
