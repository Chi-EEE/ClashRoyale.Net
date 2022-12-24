using ClashRoyale.Files;

namespace ClashRoyale.Graphics
{
    internal class Program
    {
        static Csv csv;
        static async Task Main(string[] args)
        {
            csv = new Csv();
            TrainingGame trainingGame = new();
            await trainingGame.PlayAsync();
        }
    }
}