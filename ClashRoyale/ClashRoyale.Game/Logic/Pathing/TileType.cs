using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Pathing
{
    public enum TileType
    {
        Default = 0,
        Excluded = 16,
        PathLeft = 1,
        PathRight = 2,
        KingLeft = 17,
        KingRight = 18,
        Water = 32,
        SurroundMiddleDefault = 128,
        MiddleDefault = 512,
    }
}
