using ClashRoyale.Files;
using ClashRoyale.Simulator;

namespace ClashRoyale
{
    internal class Program
    {
        public static Csv Csv { get; set; }

        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Csv = new Csv();

            Battle battle = new Battle();
            await battle.StartBattleAsync();
            Console.WriteLine("Done!");
        }
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}