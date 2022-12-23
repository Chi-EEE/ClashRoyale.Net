using System;
using System.Collections.Generic;
using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvLogic;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files
{
    public partial class Csv
    {
        public static Dictionary<Files, Type> DataTypes = new Dictionary<Files, Type>();
        public enum Files
        {
            AreaEffectObjects = 1,
            Arenas = 2,
            Buildings = 3,
            Characters = 4,
            CharacterBuffs = 5,
            ContentTests = 6,
            GameModes = 7,
            Globals = 8,
            Locations = 9,
            Npcs = 10,
            PredefinedDecks = 11,
            Projectiles = 12,
            PveGamemodes = 13,
            PveWaves = 14,
            SpellsBuildings = 15,
            SpellsCharacters = 16,
            SpellsOther = 17,
        }
        static Csv()
        {
            DataTypes.Add(Files.AreaEffectObjects, typeof(AreaEffectObjects));
            DataTypes.Add(Files.Arenas, typeof(Arenas));
            DataTypes.Add(Files.Buildings, typeof(Buildings));
            DataTypes.Add(Files.Characters, typeof(Characters));
            DataTypes.Add(Files.CharacterBuffs, typeof(CharacterBuffs));
            DataTypes.Add(Files.ContentTests, typeof(ContentTests));
            DataTypes.Add(Files.GameModes, typeof(GameModes));
            DataTypes.Add(Files.Globals, typeof(Globals));
            DataTypes.Add(Files.Locations, typeof(Locations));
            DataTypes.Add(Files.Npcs, typeof(Npcs));
            DataTypes.Add(Files.PredefinedDecks, typeof(PredefinedDecks));
            DataTypes.Add(Files.Projectiles, typeof(Projectiles));
            DataTypes.Add(Files.PveGamemodes, typeof(PveGamemodes));
            DataTypes.Add(Files.PveWaves, typeof(PveWaves));
            DataTypes.Add(Files.SpellsBuildings, typeof(SpellsBuildings));
            DataTypes.Add(Files.SpellsCharacters, typeof(SpellsCharacters));
            DataTypes.Add(Files.SpellsOther, typeof(SpellsOther));
        }

        public static Data Create(Files file, Row row, DataTable dataTable)
        {
            if (DataTypes.ContainsKey(file)) return Activator.CreateInstance(DataTypes[file], row, dataTable) as Data;

            return null;
        }
    }
}