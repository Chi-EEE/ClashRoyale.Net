using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Game.Logic.Pathing;
using ClashRoyale.Game.Types;
using Microsoft.VisualBasic.FileIO;
using System.Numerics;
using System.Xml.Serialization;

namespace ClashRoyale.Files.CsvTilemap
{
    public class Tilemap
    {
        public Grid Grid;
        public List<Entity> Buildings = new();
        private bool Map = false;
        private bool Layout = false;
        public Tilemap(string path)
        {
            Grid = new(36, 64);
            using (var reader = new TextFieldParser(path))
            {
                reader.SetDelimiters(",");
                var sectionColumn = reader.ReadFields();
                if (sectionColumn == null)
                {
                    throw new Exception("Invalid Tilemap: Couldn't get first row");
                }
                string section = sectionColumn[0];
                do
                {
                    switch (section)
                    {
                        case "Map":
                            if (!Map)
                            {
                                Map = true;
                                section = handleMap(reader);
                            }
                            else
                                throw new Exception("Invalid Tilemap: There are more than one Map");
                            break;
                        case "Layout":
                            if (!Layout)
                            {
                                Layout = true;
                                section = handleLayout(reader);
                            }
                            else
                                throw new Exception("Invalid Tilemap: There are more than one Layout");
                            break;
                        case "":
                            break;
                        default:
                            throw new Exception("Invalid Tilemap: Couldn't get first section");
                    }
                }
                while (!reader.EndOfData);
            }
        }
        private string handleMap(TextFieldParser reader)
        {
            reader.ReadFields(); // Skip the x headers
            reader.ReadFields(); // Skip the datatype headers
            uint x = 0;
            uint y = 0;
            var row = reader.ReadFields();
            while (!reader.EndOfData)
            {
                if (row == null)
                {
                    throw new Exception("Invalid Tilemap: Couldn't get Map Row");
                }
                if (row[0] != "") return row[0]; // New Section

                for (int i = 1; i < row.Length; i++)
                {
                    string? tileTypeString = row[i];
                    TileType tileType;
                    if (tileTypeString == "")
                    {
                        tileType = TileType.Default;
                    }
                    else
                    {
                        tileType = (TileType)uint.Parse(tileTypeString);
                    }
                    Grid.GetNode(x, y).TileType = tileType;
                    x++;
                }
                y++;
            }
            return "";
        }
        private string handleLayout(TextFieldParser reader)
        {
            var row = reader.ReadFields();
            while (!reader.EndOfData)
            {
                if (row == null)
                {
                    throw new Exception("Invalid Tilemap: Couldn't get Building Name");
                }
                string buildingName = row[1];
                reader.ReadFields(); // Skip headers
                reader.ReadFields(); // Skip datatypes
                while (true)
                {
                    row = reader.ReadFields();
                    if (row == null)
                    {
                        throw new Exception("Invalid Tilemap: Could get Position Data");
                    }
                    if (row[0] != "") return row[0]; // New Section
                    if (row[1] != "") break;

                    var x = uint.Parse(row[2]);
                    var y = uint.Parse(row[3]);
                    Buildings building = Csv.Tables.Get(Csv.Files.Buildings).GetData<Buildings>(buildingName);
                    Buildings.Add(new(building, building.Hitpoints, new Vector2(x, y)));
                }
            }
            return "";
        }
    }
}