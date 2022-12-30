using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class ProjectileData : Data
{
	public ProjectileData(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string Rarity { get; set; }
	public int Speed { get; set; }
	public string FileName { get; set; }
	public string ExportName { get; set; }
	public string RedExportName { get; set; }
	public string ShadowExportName { get; set; }
	public string RedShadowExportName { get; set; }
	public bool ShadowDisableRotate { get; set; }
	public string PrestigeSWF { get; set; }
	public string PrestigeExportName { get; set; }
	public string PrestigeExportName2 { get; set; }
	public string PrestigeExportName3 { get; set; }
	public string PrestigeRedExportName { get; set; }
	public string PrestigeRedExportName2 { get; set; }
	public string PrestigeRedExportName3 { get; set; }
	public int Scale { get; set; }
	public bool Homing { get; set; }
	public int HomingTime { get; set; }
	public int HomingMinDistance { get; set; }
	public string HitEffect { get; set; }
	public bool HitEffectAtProjectile { get; set; }
	public string DeathEffect { get; set; }
	public int Damage { get; set; }
	public int CrownTowerDamagePercent { get; set; }
	public int Pushback { get; set; }
	public bool PushbackAll { get; set; }
	public int Radius { get; set; }
	public int RadiusY { get; set; }
	public bool AoeToAir { get; set; }
	public bool AoeToGround { get; set; }
	public bool OnlyEnemies { get; set; }
	public bool OnlyOwnTroops { get; set; }
	public int MaximumTargets { get; set; }
	public int Gravity { get; set; }
	public string SpawnAreaEffectObject { get; set; }
	public int SpawnCharacterLevelIndex { get; set; }
	public int SpawnCharacterDeployTime { get; set; }
	public string SpawnDeployBaseAnim { get; set; }
	public string SpawnCharacter { get; set; }
	public bool SpawnConstPriority { get; set; }
	public int SpawnCharacterCount { get; set; }
	public string TargetBuff { get; set; }
	public int BuffTime { get; set; }
	public int BuffTimeIncreasePerLevel { get; set; }
	public bool ApplyBuffBeforeDamage { get; set; }
	public string TrailEffect { get; set; }
	public int ProjectileRadius { get; set; }
	public int ProjectileRadiusY { get; set; }
	public int ProjectileStartExtraRadius { get; set; }
	public int ProjectileRange { get; set; }
	public bool use360Frames { get; set; }
	public string HitSoundWhenParentAlive { get; set; }
	public string SpawnProjectile { get; set; }
	public int SpawnCount { get; set; }
	public int SpawnRadius { get; set; }
	public int SpawnChain { get; set; }
	public bool SpawnAxisX { get; set; }
	public bool SpawnAxisY { get; set; }
	public int MinDistance { get; set; }
	public int MaxDistance { get; set; }
	public int ConstantHeight { get; set; }
	public bool HeightFromTargetRadius { get; set; }
	public int Heal { get; set; }
	public int CrownTowerHealPercent { get; set; }
	public bool TargetToEdge { get; set; }
	public int ChainedHitRadius { get; set; }
	public int ChainedHitCount { get; set; }
	public string ChainedHitEndEffect { get; set; }
	public string PingpongDeathEffect { get; set; }
	public bool ShakesTargets { get; set; }
	public int ShakesShooter { get; set; }
	public int PingpongVisualTime { get; set; }
	public int RandomAngle { get; set; }
	public int RandomDistance { get; set; }
	public int RandomOffset { get; set; }
	public string Scatter { get; set; }
	public int DragBackSpeed { get; set; }
	public int DragSelfSpeed { get; set; }
	public bool DragBackAsAttractor { get; set; }
	public int DragMargin { get; set; }
	public string DragEffect { get; set; }
	public string TargettedEffect { get; set; }
	public int DamagingDistance { get; set; }
	public int VariableDamage { get; set; }
	public int VariableDamageDistance { get; set; }
	public string VariableDamageHitEffect { get; set; }
	public bool CheckCollisions { get; set; }
	public int RandomDelay { get; set; }
	public int AngularDelay { get; set; }
	public int StartCircleRadius { get; set; }
	public int TargetCircleRadius { get; set; }
	public int CircleSpeed { get; set; }
	public int PingpongMovingShooter { get; set; }
	public bool HideWhenDelayed { get; set; }
	public string DeflectBehaviour { get; set; }
	public int DeflectRadius { get; set; }
	public int DeflectRadiusY { get; set; }
	}
}
