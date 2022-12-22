using ClashRoyale.Files;

namespace ClashRoyale
{
    internal class Program
    {
        public static Csv Csv { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Csv = new Csv();
        }
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}