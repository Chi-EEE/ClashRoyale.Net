using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class CharacterBuffs : Data
{
	public CharacterBuffs(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string Rarity { get; set; }
	public string TID { get; set; }
	public string IconFileName { get; set; }
	public string IconExportName { get; set; }
	public bool NoEffectToCrownTowers { get; set; }
	public int BuildingDamagePercent { get; set; }
	public int CrownTowerDamagePercent { get; set; }
	public int DamagePerSecond { get; set; }
	public int HitFrequency { get; set; }
	public int DamageReduction { get; set; }
	public int HealPerSecond { get; set; }
	public int HitSpeedMultiplier { get; set; }
	public int SpeedMultiplier { get; set; }
	public int SpawnSpeedMultiplier { get; set; }
	public bool Invisible { get; set; }
	public bool RemoveOnAttack { get; set; }
	public bool RemoveOnHeal { get; set; }
	public bool RemoveOnHit { get; set; }
	public int DamageOnHit { get; set; }
	public int DamageMultiplier { get; set; }
	public string Effect { get; set; }
	public string FilterFile { get; set; }
	public string FilterExportName { get; set; }
	public bool FilterAffectsTransformation { get; set; }
	public bool FilterInheritLifeDuration { get; set; }
	public bool IgnorePushBack { get; set; }
	public string MarkEffect { get; set; }
	public int AudioPitchModifier { get; set; }
	public bool Portal { get; set; }
	public int AttractPercentage { get; set; }
	public int LateralPushPercentage { get; set; }
	public int PushMassFactor { get; set; }
	public int PushSpeedFactor { get; set; }
	public bool ControlledByParent { get; set; }
	public bool Clone { get; set; }
	public int Scale { get; set; }
	public bool EnableStacking { get; set; }
	public string ChainedBuff { get; set; }
	public int ChainedBuffTime { get; set; }
	public string ContinuousEffect { get; set; }
	public bool Charge { get; set; }
	public int SpawnStartTime { get; set; }
	public int SpawnNumber { get; set; }
	public int SpawnInterval { get; set; }
	public int SpawnPauseTime { get; set; }
	public string SpawnObject { get; set; }
	public int SpawnLimit { get; set; }
	public int SpawnLevelIndex { get; set; }
	public int HitpointMultiplier { get; set; }
	public int Shield { get; set; }
	public bool Jump { get; set; }
	public string Projectile { get; set; }
	public string ProjectileEffect { get; set; }
	public string RemoveEffect { get; set; }
	public string OverrideProjectile { get; set; }
	public bool HitTickFromSource { get; set; }
	public string MorphTarget { get; set; }
	public string DeathSpawn { get; set; }
	public bool DeathSpawnIsEnemy { get; set; }
	public int DeathSpawnCount { get; set; }
	public bool DeathSpawnDeployDelay { get; set; }
	public bool IgnoreBuildings { get; set; }
	public bool LockTarget { get; set; }
	public bool Rally { get; set; }
	public bool NotCloned { get; set; }
	public int LevelIncrease { get; set; }
	public bool ApplyFear { get; set; }
	}
}
