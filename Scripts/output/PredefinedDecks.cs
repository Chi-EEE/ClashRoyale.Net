using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
	{
	public PredefinedDecks(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string Spells { get; set; }
	public int SpellLevel { get; set; }
	public string RandomSpellSets { get; set; }
	public string Description { get; set; }
	public string TID { get; set; }
}
