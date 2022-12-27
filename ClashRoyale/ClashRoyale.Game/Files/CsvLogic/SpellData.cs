using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class SpellData : Data
{
	public SpellData(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public int DeployStartY { get; set; }
	public string PveDefenseType { get; set; }
	public bool ManaCostFromSummonerMana { get; set; }
	public int Pushback { get; set; }
	public string Projectile { get; set; }
	public string BuffOnDamage { get; set; }
	public int InstantDamage { get; set; }
	public bool DarkMirror { get; set; }
	public int DurationSeconds { get; set; }
	public int Radius { get; set; }
	public string IndicatorPrestigeExportName { get; set; }
	public int DarkElixirCost { get; set; }
	public string Rarity { get; set; }
	public string IconSWFChina { get; set; }
	public int SummonDeployDelay { get; set; }
	public string UnlockArena { get; set; }
	public string CustomFirstProjectile { get; set; }
	public string NextCardReplace { get; set; }
	public bool NotInUse { get; set; }
	public bool FullLaneDeploy { get; set; }
	public bool CanPlaceOnWater { get; set; }
	public int InstantHeal { get; set; }
	public string IndicatorPrestigeExportName2 { get; set; }
	public int BuffTimeIncreasePerLevel { get; set; }
	public string IndicatorEffect { get; set; }
	public int DeployEndY { get; set; }
	public string BuffType { get; set; }
	public string CharacterSkin { get; set; }
	public string DestIndicatorEffect { get; set; }
	public int SummonCharacterLevelIndex { get; set; }
	public string IconFile { get; set; }
	public int ProjectileWaves { get; set; }
	public bool NotVisible { get; set; }
	public int UnlockLevel { get; set; }
	public int DamageModifier { get; set; }
	public int ProjectileInterval { get; set; }
	public string TID_INFO { get; set; }
	public string HighresImageFilename { get; set; }
	public string SummonCharacter { get; set; }
	public int PrestigeCount { get; set; }
	public string TID { get; set; }
	public string Effect { get; set; }
	public int SummonNumber { get; set; }
	public string IconSWF { get; set; }
	public string IndicatorPrestigeSWF { get; set; }
	public bool Mirror { get; set; }
	public bool SpellAsDeploy { get; set; }
	public bool HideRadiusIndicator { get; set; }
	public int Height { get; set; }
	public int DeployWTileMargin { get; set; }
	public int MultipleProjectiles { get; set; }
	public bool StatsUnderInfo { get; set; }
	public int BuffTime { get; set; }
	public bool OnlyOwnTroops { get; set; }
	public string SummonCharacterSecond { get; set; }
	public int ProjectileAngle { get; set; }
	public int ElixirProductionStopTime { get; set; }
	public string Name { get; set; }
	public string Tribe { get; set; }
	public int BuffNumber { get; set; }
	public int SummonDeployDelaySecond { get; set; }
	public string PassiveAbility { get; set; }
	public bool TouchdownLimitedDeploy { get; set; }
	public int AbilityCooldown { get; set; }
	public bool CanPlaceOnBuildings { get; set; }
	public int SummonWidth { get; set; }
	public int CustomDeployTime { get; set; }
	public int ProjectileWaveInterval { get; set; }
	public string AreaEffectObject { get; set; }
	public string CharacterSkin2 { get; set; }
	public bool OnlyEnemies { get; set; }
	public int HealPerSecond { get; set; }
	public bool CanDeployOnEnemySide { get; set; }
	public int SummonRadius { get; set; }
	public string HighresPlaceholderAsset { get; set; }
	public string ReleaseDate { get; set; }
	public string IndicatorPrestigeExportName3 { get; set; }
	public string CastSound { get; set; }
	public string HeroAbility { get; set; }
	public int ManaCost { get; set; }
	public int HPModifier { get; set; }
	public string IndicatorFileName { get; set; }
	public int SummonCharacterSecondCount { get; set; }
}
}
