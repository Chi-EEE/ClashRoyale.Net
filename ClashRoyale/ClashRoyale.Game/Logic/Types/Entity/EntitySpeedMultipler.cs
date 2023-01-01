using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Types.Entity
{
    public partial class EntityContext
    {
        private bool Charging { get; set; }
        private bool Raged { get; set; } // 35% boost on speed and attack speed and spawner speeds and Elixir Collector's production. However, Rage also reduces building lifetime
        private bool Slowed { get; set; } // 15% more slowly the Elixir Collector to be affected by the Poison's slowing
        private bool Frozen { get; set; } // No move
    }
}
