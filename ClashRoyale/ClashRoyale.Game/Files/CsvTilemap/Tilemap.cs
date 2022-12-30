using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files;
using ClashRoyale.Game.Logic.Pathing;
using Microsoft.VisualBasic.FileIO;
using System.Numerics;
using ClashRoyale.Game.Logic;
using ClashRoyale.Game.Logic.Types.Entity;

namespace ClashRoyale.Game.Files.CsvTilemap
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
            do
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
                    x = (uint)((x + 1) % Grid.Width);
                }
                y = (uint)((y + 1) % Grid.Height);
                row = reader.ReadFields();
            }
            while (!reader.EndOfData);
            return "";
        }
        private string handleLayout(TextFieldParser reader)
        {
            var row = reader.ReadFields();
            do
            {
                if (row == null)
                {
                    throw new Exception("Invalid Tilemap: Couldn't get Building Name");
                }
                string buildingName = row[1];
                reader.ReadFields(); // Skip headers
                reader.ReadFields(); // Skip datatypes
                var centerOfArena = new Vector2(Arena.REAL_ARENA_WIDTH, Arena.REAL_ARENA_HEIGHT) / 2.0f;
                do
                {
                    row = reader.ReadFields();
                    if (row == null)
                    {
                        throw new Exception("Invalid Tilemap: Could get Position Data");
                    }
                    if (row[0] != "") return row[0]; // New Section
                    if (row[1] != "") break; // Must of gotten a building name

                    var x = uint.Parse(row[2]);
                    var y = uint.Parse(row[3]);
                    EntityData building = Csv.Tables.Get(Csv.Files.Buildings).GetData<EntityData>(buildingName);
                    Team team = Team.None;
                    if (y <= 33)
                    {
                        team = Team.Red;
                    }
                    else if (y >= 34)
                    {
                        team = Team.Blue;
                    }

                    Buildings.Add(new(building, building.Hitpoints, new Vector2((x * 500) - centerOfArena.X, (y * 500) - centerOfArena.Y), team));
                }
                while (!reader.EndOfData);
            }
            while (!reader.EndOfData);
            return "";
        }
    }
}
