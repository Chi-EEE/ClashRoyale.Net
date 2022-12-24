using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClashRoyale.Game
{
    class CSV
    {
        static CSV()
        {
            var fileNames = Directory.GetFiles("GameAssets/csv_logic/");
            foreach (var file in fileNames)
            {
                using (TextFieldParser csvParser = new TextFieldParser(file))
                {
                    //csvParser.Parse(file);
                }
            }
        }
    }
}