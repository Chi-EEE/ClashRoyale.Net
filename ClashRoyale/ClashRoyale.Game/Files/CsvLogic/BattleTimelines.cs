using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class BattleTimelines : Data
{
	public BattleTimelines(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public List<int> SectionLength { get; set; }
	public List<string> SectionType { get; set; }
	public List<string> SectionFlags { get; set; }
	public int StartingElixir { get; set; }
	public List<int> ElixirRateLength { get; set; }
	public List<int> ElixirFullBarMS { get; set; }
	public List<int> ElixirRateVisible { get; set; }
	public List<bool> ElixirNotifyChange { get; set; }
	public List<int> NextSpellCooldownLength { get; set; }
	public List<int> NextSpellCooldownMS { get; set; }
	public List<int> EventTime { get; set; }
	public List<string> EventType { get; set; }
	public List<int> EventIntParameter1 { get; set; }
	public List<string> EventStringParameter1 { get; set; }
	}
}
