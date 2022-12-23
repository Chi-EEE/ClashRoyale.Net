using System.Collections.Generic;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files
{
    public partial class Csv
    {
        public static readonly List<string> Gamefiles = new List<string>();
        public static Gamefiles Tables;

        public Csv()
        {
            var fileNames = Directory.GetFiles("GameAssets/csv_logic/");
            foreach (var file in fileNames)
                Gamefiles.Add(file);

            Tables = new Gamefiles();

            foreach (var file in Gamefiles)
                Tables.Initialize(new Table(file), (Files) Gamefiles.IndexOf(file) + 1);
        }
    }
}