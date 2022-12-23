using ClashRoyale.Files;
using ClashRoyale.Files.CsvLogic;
using System;
using System.Numerics;

namespace ClashRoyale.Simulator.Types
{
    public class TroopContext
    {
        public Arena Arena { get; set; }
        public Troop Troop { get; set; }
        public Characters Character { get; set; }
        public Vector2 Position { get; set; }
        public Troop? Target { get; set; }

        public TroopContext(Arena arena, Characters character, Vector2 position)
        {
            Arena = arena;
            Troop = new Troop(0, character.Hitpoints, character.DeployTime);
            Character = character;
            Position = position;
        }

        public void Tick()
        {
            if (Troop == null)
            {
                Console.WriteLine("TROOP NULL!");
                return;
            }

            Move(0, Character.Speed);
        }

        public void Move(int x, int y)
        {
            var newX = Position.X + x;
            var newY = Position.Y + y;

            Position = new Vector2(newX, newY);

            //Console.WriteLine($"{Character.Name} moved to {Position}");
        }
    }
}