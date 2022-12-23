using ClashRoyale.Files;
using ClashRoyale.Simulator;

namespace ClashRoyale
{
    internal class Program
    {
        public static Csv Csv { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Csv = new Csv();

            Battle battle = new Battle();
            battle.Start();
            Console.WriteLine("Done!");
        }
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}