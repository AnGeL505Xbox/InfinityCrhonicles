using System.Collections.Generic;

#region Enums
public enum ElementType { NORMAL, LUMEN, UMBRA, IGNIS, AQUA, TERRA, ELEKTRO, CRYO, NATURA, VORTEX, PSYCHIC }
public enum ElementMix
{
    NULL, LUMBRA, IGNTERRA, AQYO, UNKNOW,

    VORTERRA, CRYEX, UMBREX, LUMREX, VORQUA, NATUREX, VORESIS
}
public enum Rarity { COMMON, UNCOMMON, RARE, ULTRARARE, LEGENDARY }

//SkillType
public enum HealthModType { STAT_BASE, FIXED, PERCENTAGE }
public enum StatusModType { ATTACK_MOD, DEFENSE_MOD, SPEED_MOD, EVASION_MOD }

//Tags
public enum SkillType { NORMAL, ULTIMATE }
public enum SingularSkillTag { NULL, ADAPTATION, ABSORPTION, LIGTH_SPEED, REPULSION, FIRMNESS, BLOODY_NIGTH, IGNIS_FATUUS, TELEKINESIS, ELEKTROKINESIS, STELLAR_FLASH, BRINICLE, OBSIDIAN_SCALES, ATOMIC_FUSION, PHOTOSYNTHESIS }
public enum SpiritTag { NULL, NORDIC, GREEK, EGIPT, ABRAHMIC, AZTEC }
public enum BattleTag { Tutorial1, Tutorial2, TutorialFinal, BattleNormal, BattleBoss }
public enum DificultyTag { Easy, Normal, Hard }

//Status
public enum TutorialStatus { START, STORY, BATTLE, CARDINV, ROULETTE, END, NULL }
public enum CombatStatus { START_COMBAT, TURN, WAITING_FOR_TURN, ENTITY_ACTION, CHECK_VICTORY }
#endregion

[System.Serializable]
public class Globals
{
    //PlayerVariables
    public CardFigther currentPlayerFigther;
    public List<CardFigther> playerFigtherCards;
    public List<CardSpirit> playerSpiritCards;
    public int goldCoin = 0;

    public int stellarCoin = 0;
    public int stellarFragment = 0;

    public List<string> playerCodes = new() { "Null", "DebugPls", "Lumity" };

    //GameVariables
    public TutorialStatus tutorial = TutorialStatus.START;

    public List<CardFigther> tFigtherCard;
    public List<CardSpirit> tSpiritCard;

    public List<CardFigther> tEventFig;
    public List<CardSpirit> tEventSpirit;

    public List<CardFigther> totalFigthers;
    public List<CardSpirit> totalSpirits;

    public List<float> stages = new();
    public List<int> chapters = new();
}