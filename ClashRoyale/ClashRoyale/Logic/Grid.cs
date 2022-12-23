using ClashRoyale.Files.CsvReader;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClashRoyale
{
    public class Grid
    {
        public List<List<Node>> grid { get; set; }
        public Grid(int width, int height)
        {
            Construct(width, height);
            Connect(width, height);
        }
        private void Construct(int width, int height)
        {
            grid = new();
            for (int x = 0; x < width; x++)
            {
                grid[x] = new();
                for (int y = 0; y < height; y++)
                {
                    grid[x][y] = new(x, y);
                }
            }
        }
        private void Connect(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                var xAxis = grid[x];
                for (int y = 0; y < height; y++)
                {
                    ConnectNode(width, height, xAxis[y], x, y);
                }
            }
        }
        private void ConnectNode(int width, int height, Node node, int x, int y)
        {
            if (x - 1 >= 0)
            {
                node.Neighbours[3] = grid[x - 1][y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = grid[x - 1][y - 1];
                }
                if (y + 1 < height)
                {
                    node.Neighbours[5] = grid[x - 1][y + 1];
                }
            }
            if (x + 1 < width)
            {
                node.Neighbours[3] = grid[x + 1][y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = grid[x + 1][y - 1];
                }
                if (y + 1 < height)
                {
                    node.Neighbours[5] = grid[x + 1][y + 1];
                }
            }
            if (y - 1 >= 0)
            {
                node.Neighbours[1] = grid[x][y - 1];
            }
            if (y + 1 < height)
            {
                node.Neighbours[5] = grid[x][y + 1];
            }
        }
        public Node GetNodeNearVector2(Vector2 vector2)
        {
            var xAxis = grid[Math.Clamp(Convert.ToInt32(vector2.X), 0, grid.Count)];
            return xAxis[Math.Clamp(Convert.ToInt32(vector2.Y), 0, xAxis.Count)];
        }
    }
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
        public Node[] Neighbours = new Node<T>[8];

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}