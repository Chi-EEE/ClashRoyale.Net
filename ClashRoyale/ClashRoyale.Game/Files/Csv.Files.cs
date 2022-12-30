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
            BattleTimelines = 3,
            Buildings = 4,
            Characters = 5,
            CharacterAbilities = 6,
            CharacterBuffs = 7,
            ContentTests = 8,
            GameModes = 9,
            Globals = 10,
            Locations = 11,
            Projectiles = 12,
            PveGamemodes = 13,
            SpellsBuildings = 14,
            SpellsCharacters = 15,
            SpellsOther = 16,
            SpellSets = 17,
        }
        static Csv()
        {
            DataTypes.Add(Files.AreaEffectObjects, typeof(AreaEffectObjects));
            DataTypes.Add(Files.Arenas, typeof(Arenas));
            DataTypes.Add(Files.BattleTimelines, typeof(BattleTimelines));
            DataTypes.Add(Files.Buildings, typeof(EntityData));
            DataTypes.Add(Files.Characters, typeof(EntityData));
            DataTypes.Add(Files.CharacterAbilities, typeof(CharacterAbilities));
            DataTypes.Add(Files.CharacterBuffs, typeof(CharacterBuffs));
            DataTypes.Add(Files.ContentTests, typeof(ContentTests));
            DataTypes.Add(Files.GameModes, typeof(GameModes));
            DataTypes.Add(Files.Globals, typeof(Globals));
            DataTypes.Add(Files.Locations, typeof(Locations));
            DataTypes.Add(Files.Projectiles, typeof(ProjectileData));
            DataTypes.Add(Files.PveGamemodes, typeof(PveGamemodes));
            DataTypes.Add(Files.SpellsBuildings, typeof(SpellData));
            DataTypes.Add(Files.SpellsCharacters, typeof(SpellData));
            DataTypes.Add(Files.SpellsOther, typeof(SpellData));
            DataTypes.Add(Files.SpellSets, typeof(SpellSets));
        }
        public static Data Create(Files file, Row row, DataTable dataTable)
        {
            // If an error occurs here, make sure to clean the solution and run it again otherwise update this class and generate new CSV class files in the Scripts folder [CSV_Definer.py]
            if (DataTypes.ContainsKey(file)) return Activator.CreateInstance(DataTypes[file], row, dataTable) as Data;

            return null;
        }
    }
}