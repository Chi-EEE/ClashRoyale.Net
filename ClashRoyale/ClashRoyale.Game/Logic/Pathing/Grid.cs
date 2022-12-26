using System.Numerics;

namespace ClashRoyale.Game.Logic.Pathing
{
    public class Grid
    {
        public Node[,] nodes { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public Grid(int width, int height)
        {
            Width = width;
            Height = height;
            ConstructNodes();
            ConnectNodes();
        }
        public Node GetNode(uint x, uint y)
        {
            return nodes[x, y];
        }
        private void ConstructNodes()
        {
            nodes = new Node[Width, Height];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    nodes[x, y] = new(x, y);
                }
            }
        }
        private void ConnectNodes()
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    ConnectNode(Width, Height, nodes[x, y], x, y);
                }
            }
        }
        private void ConnectNode(int Width, int Height, Node node, int x, int y)
        {
            if (x - 1 >= 0)
            {
                node.Neighbours[3] = nodes[x - 1, y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = nodes[x - 1, y - 1];
                }
                if (y + 1 < Height)
                {
                    node.Neighbours[5] = nodes[x - 1, y + 1];
                }
            }
            if (x + 1 < Width)
            {
                node.Neighbours[3] = nodes[x + 1, y];
                if (y - 1 >= 0)
                {
                    node.Neighbours[1] = nodes[x + 1, y - 1];
                }
                if (y + 1 < Height)
                {
                    node.Neighbours[5] = nodes[x + 1, y + 1];
                }
            }
            if (y - 1 >= 0)
            {
                node.Neighbours[1] = nodes[x, y - 1];
            }
            if (y + 1 < Height)
            {
                node.Neighbours[5] = nodes[x, y + 1];
            }
        }
        public Node GetNodeNearVector2(Vector2 vector2)
        {
            return nodes[Math.Clamp(Convert.ToInt32(vector2.X), 0, Width), Math.Clamp(Convert.ToInt32(vector2.Y), 0, Height)];
        }
    }
  
}