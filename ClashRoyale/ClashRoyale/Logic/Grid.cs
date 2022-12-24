using ClashRoyale.Files.CsvReader;
using System.Numerics;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ClashRoyale
{
    public class Grid
    {
        public Node[,] grid { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        public Grid(int width, int height)
        {
            Width = width; 
            Height = height;
            Construct();
            Connect();
        }
        private void Construct()
        {
            grid = new Node[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    grid[x, y] = new(x, y);
                }
            }
        }
        private void Connect()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ConnectNode(Width, Height, grid[x, y], x, y);
                }
            }
        }
        private void ConnectNode(int Width, int Height, Node node, int x, int y)
        {
            if (x - 1 >= 0)
            {
                node.Neighbours[3] = grid[x - 1, y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = grid[x - 1, y - 1];
                }
                if (y + 1 < Height)
                {
                    node.Neighbours[5] = grid[x - 1, y + 1];
                }
            }
            if (x + 1 < Width)
            {
                node.Neighbours[3] = grid[x + 1, y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = grid[x + 1, y - 1];
                }
                if (y + 1 < Height)
                {
                    node.Neighbours[5] = grid[x + 1, y + 1];
                }
            }
            if (y - 1 >= 0)
            {
                node.Neighbours[1] = grid[x, y - 1];
            }
            if (y + 1 < Height)
            {
                node.Neighbours[5] = grid[x, y + 1];
            }
        }
        public Node GetNodeNearVector2(Vector2 vector2)
        {
            return grid[Math.Clamp(Convert.ToInt32(vector2.X), 0, Width), Math.Clamp(Convert.ToInt32(vector2.Y), 0, Height)];
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
        public Node[] Neighbours = new Node[8];

        public Node(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}