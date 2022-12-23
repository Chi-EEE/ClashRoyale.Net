using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class Arenas : Data
{
	public Arenas(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string TID { get; set; }
	public string SubtitleTID { get; set; }
	public int Arena { get; set; }
	public int League { get; set; }
	public string LeagueArenaSWF { get; set; }
	public string LeagueArenaExportName { get; set; }
	public string ChestArena { get; set; }
	public string TvArena { get; set; }
	public bool IsInUse { get; set; }
	public bool TrainingCamp { get; set; }
	public bool PVEArena { get; set; }
	public int LoseTrophyPercentage { get; set; }
	public int LoseTrophyScore { get; set; }
	public int TrophyLimit { get; set; }
	public int DemoteTrophyLimit { get; set; }
	public int ChestRewardMultiplier { get; set; }
	public int ShopChestRewardMultiplier { get; set; }
	public int RequestSize { get; set; }
	public int MaxDonationCountCommon { get; set; }
	public int MaxDonationCountRare { get; set; }
	public int MaxDonationCountEpic { get; set; }
	public string IconSWF { get; set; }
	public string IconExportName { get; set; }
	public string SmallIconExportName { get; set; }
	public int MatchmakingMinTrophyDelta { get; set; }
	public int MatchmakingMaxTrophyDelta { get; set; }
	public int MatchmakingMaxSeconds { get; set; }
	public string PvpLocation { get; set; }
	public string TeamVsTeamLocation { get; set; }
	public string ThreeVsThreeLocation { get; set; }
	public int DailyDonationCapacityLimit { get; set; }
	public int BattleRewardGold { get; set; }
	public string ReleaseDate { get; set; }
	public bool DisableIn2v2 { get; set; }
	public string ReachedBoost { get; set; }
	public int BoostTimeH { get; set; }
	public bool BgGlow { get; set; }
	public bool Glow { get; set; }
	public bool Water { get; set; }
	public string WaterParamMapPostfix { get; set; }
	public string BackgroundSWF { get; set; }
	public string BackgroundExportName { get; set; }
	public string BackgroundOverlayExportName { get; set; }
	public string FixedTowerSkinSet { get; set; }
	public int MinScale { get; set; }
	public int LeagueBadgeOffsetY { get; set; }
	public bool ChestWildcardsEnabled { get; set; }
	public int ChestWildcardPercent100Common { get; set; }
	public int ChestWildcardPercent100Rare { get; set; }
	public int ChestWildcardPercent100Epic { get; set; }
	public int ChestWildcardPercent100Legendary { get; set; }
	public int ChestWildcardPercent100Champion { get; set; }
	public int RequiredKingLevel { get; set; }
	}
}
