using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Types
{
    public class Entity
    {
        public int Level { get; set; }
        public int Hitpoints { get; set; }
        public float DeployTime { get; set; }
        public Entity(int level, int hitpoints, float deployTime = 0)
        {
            Level = level;
            Hitpoints = hitpoints;
            DeployTime = deployTime;
        }
    }
}
