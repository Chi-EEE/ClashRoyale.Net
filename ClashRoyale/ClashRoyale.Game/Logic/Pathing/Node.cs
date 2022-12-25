using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game.Logic.Pathing
{
    public class Node
    {
        public int X { get; }
        public int Y { get; }
        /// <summary>
        /// X | X | X
        /// X | H | X
        /// X | X | X
        /// 
        /// 0 | 1 | 2
        /// 3 | H | 4
        /// 5 | 6 | 7
        /// </summary>
        public Node[] Neighbours = new Node[8];

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
