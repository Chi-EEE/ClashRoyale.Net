using ClashRoyale.Files.CsvHelpers;
using ClashRoyale.Files.CsvReader;

namespace ClashRoyale.Files.CsvLogic
{
public class GameModes : Data
{
	public GameModes(Row row, DataTable datatable) : base(row, datatable)
	{
		LoadData(this, GetType(), row);
	}
	public string Name { get; set; }
	public string TID { get; set; }
	public string CardLevelAdjustment { get; set; }
	public int PlayerCount { get; set; }
	public string DeckSelection { get; set; }
	public string BattleTimeline { get; set; }
	public string PredefinedDecks { get; set; }
	public bool SameDeckOnBoth { get; set; }
	public bool SeparateTeamDecks { get; set; }
	public bool SwappingTowers { get; set; }
	public bool RandomBoosts { get; set; }
	public string ForcedDeckCards { get; set; }
	public string ForcedCardInOpeningHand { get; set; }
	public string Players { get; set; }
	public string EventDeckSetLimit { get; set; }
	public bool EventDeckClanWar { get; set; }
	public bool ForcedDeckCardsUsingCardTheme { get; set; }
	public int GoldPerTower1 { get; set; }
	public int GoldPerTower2 { get; set; }
	public int GoldPerTower3 { get; set; }
	public int ExtraCrownsPerTower1 { get; set; }
	public int ExtraCrownsPerTower2 { get; set; }
	public int ExtraCrownsPerTower3 { get; set; }
	public string EndConfetti1 { get; set; }
	public string EndConfetti2 { get; set; }
	public int TargetTouchdowns { get; set; }
	public bool FixedDeckOrder { get; set; }
	public string Icon { get; set; }
	public string FixedArena { get; set; }
	public string ClanWarDescription { get; set; }
	public string DescriptionTID { get; set; }
	public int BattleStartCooldown { get; set; }
	public string GlobalBuff { get; set; }
	public string LoopingEffect { get; set; }
	public bool Heist { get; set; }
	public string SkinSet { get; set; }
	public string SkinAddDeathEffect { get; set; }
	public string SkinAddDeathEffectKing { get; set; }
	public string SpawnCharacter { get; set; }
	public string SpawnSpell { get; set; }
	public string SpawnPattern { get; set; }
	public bool SpawnByTurns { get; set; }
	public int SpawnCharacterInterval { get; set; }
	public int SpawnCharacterCount { get; set; }
	public int SpawnCharacterStartDelay { get; set; }
	public int SpawnCharacterMaxExtraDelay { get; set; }
	public bool SpawnCharacterToArea { get; set; }
	public bool SpawnCharacterNeutral { get; set; }
	public bool SpawnCharacterSameSide { get; set; }
	public string SpawnCharacterSpawnEffect { get; set; }
	public string SpawnProjectile { get; set; }
	public int SpawnMaxInstances { get; set; }
	public string DeckSpellSets { get; set; }
	public string AntiSpellSets { get; set; }
	public bool SpellSupport { get; set; }
	public string BuffCharacter { get; set; }
	public string BuffName { get; set; }
	public string CapturableBuilding { get; set; }
	public int CapturableBuildingRespawnInterval { get; set; }
	public int ElixirSpawnInterval { get; set; }
	public int DeckSize { get; set; }
	public string NeutralCharacter { get; set; }
	public bool ValidChallengeMode { get; set; }
	public bool ValidFriendlyMode { get; set; }
	public bool ValidLadderMode { get; set; }
	public bool ValidPartyMode { get; set; }
	public bool NoDraws { get; set; }
	public bool BestOfGame { get; set; }
	public int BanDecksGame { get; set; }
	public string ReplacePrincessTower { get; set; }
	public string ReplaceKingTower { get; set; }
	public bool RandomElixirCost { get; set; }
	public int RandomElixirCostMin { get; set; }
	public int RandomElixirCostMax { get; set; }
	public int RandomElixirCostNoDuplicateLookahead { get; set; }
	public string Boosts { get; set; }
	public bool PermanentBoosts { get; set; }
	public string BoostSpawnPattern { get; set; }
	public int BoostSpawnFrequency { get; set; }
	public int BoostSpawnInitialDelay { get; set; }
	public bool PickModeCompetitivePattern { get; set; }
	public bool Overtime { get; set; }
	public bool RampUpSpawnCount { get; set; }
	public bool RampUpSpawnInterval { get; set; }
	public bool TripleElixir { get; set; }
	public bool MirrorDeck { get; set; }
	public string WinAwardBadge { get; set; }
	public string WinAwardBadge2 { get; set; }
	public bool ShowTrophies { get; set; }
	public string PickData { get; set; }
	}
}
