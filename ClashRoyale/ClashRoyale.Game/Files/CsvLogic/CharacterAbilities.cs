using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class CharacterAbilities : Data
{
	public CharacterAbilities(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string TID { get; set; }
	public string TID_INFO { get; set; }
	public string IconSWF { get; set; }
	public string IconExportName { get; set; }
	public int CastTime { get; set; }
	public int TriggerDelay { get; set; }
	public int ManaCost { get; set; }
	public int Cooldown { get; set; }
	public string PendingBuff { get; set; }
	public string Buff { get; set; }
	public int BuffTime { get; set; }
	public int BuffRadius { get; set; }
	public string AreaEffectObject { get; set; }
	public string MorphTarget { get; set; }
	public string ResurrectGainChargeEffect { get; set; }
	public string ResurrectChargeFilter { get; set; }
	public string ResurrectHealthBar { get; set; }
	public bool ResurrectEnemies { get; set; }
	public bool ResurrectOwnTroops { get; set; }
	public int ResurrectBaseCount { get; set; }
	public int SpawnLimit { get; set; }
	public int DashRange { get; set; }
	public bool DashTargetFurthest { get; set; }
	public bool SwitchLanes { get; set; }
	public string DeployedEffect { get; set; }
	public string DeployedClip { get; set; }
	public string ActivationSpawnCharacter { get; set; }
	public int ActivationSpawnDeployTime { get; set; }
	public int AbilityStateDuration { get; set; }
	}
}
