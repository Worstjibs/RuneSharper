using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RuneSharper.Shared.Enums;

public enum ActivityType
{
    [Display(Name = "League Points")]
    LeaguePoints, 
    [Display(Name = "Bounty Hunter Rogues")]
    BountyHunterRogues,
    [Display(Name = "Bounty Hunter")]
    BountyHunter,
    [Display(Name = "Total Clues Scrolls")]
    TotalCluesScrolls,
    [Display(Name = "Beginner Clue Scrolls")]
    BeginnerClueScrolls,
    [Display(Name = "Easy Clue Scrolls")]
    EasyClueScrolls,
    [Display(Name = "Medium Clue Scrolls")]
    MediumClueScrolls,
    [Display(Name = "Hard Clue Scrolls")]
    HardClueScrolls,
    [Display(Name = "Elite Clue Scrolls")]
    EliteClueScrolls,
    [Display(Name = "Master Clue Scrolls")]
    MasterClueScrolls,
    [Display(Name = "Last Man Standing")]
    LastManStanding,
    [Display(Name = "PvP Arena")]
    PvPArena,
    [Display(Name = "Soul Wars Zeal")]
    SoulWarsZeal,
    [Display(Name = "Rifts Closed")]
    RiftsClosed,
    [Display(Name = "Abyssal Sire")]
    AbyssalSire,
    [Display(Name = "Alchemical Hydra")]
    AlchemicalHydra,
    [Display(Name = "Barrows Chests")]
    BarrowsChests,
    Bryophyta,
    Callisto,
    Cerberus,
    [Display(Name = "Chambers of Xeric")]
    ChambersofXeric,
    [Display(Name = "Chambers of Xeric Challenge Mode")]
    ChambersofXericChallengeMode,
    [Display(Name = "Chaos Elemental")]
    ChaosElemental,
    [Display(Name = "Chaos Fanatic")]
    ChaosFanatic,
    [Display(Name = "Commander Zilyana")]
    CommanderZilyana,
    [Display(Name = "Corporeal Beast")]
    CorporealBeast,
    [Display(Name = "Crazy Archaeologist")]
    CrazyArchaeologist,
    [Display(Name = "Dagannoth Prime")]
    DagannothPrime,
    [Display(Name = "Dagannoth Rex")]
    DagannothRex,
    [Display(Name = "Dagannoth Supreme")]
    DagannothSupreme,
    [Display(Name = "Deranged Archaeologist")]
    DerangedArchaeologist,
    [Display(Name = "General Graardor")]
    GeneralGraardor,
    [Display(Name = "Giant Mole")]
    GiantMole,
    [Display(Name = "Grotesque Guardians")]
    GrotesqueGuardians,
    Hespori,
    [Display(Name = "Kalphite Queen")]
    KalphiteQueen,
    [Display(Name = "King Black Dragon")]
    KingBlackDragon,
    Kraken,
    Kree,
    Kril,
    Mimic,
    Nex,
    Nightmare,
    [Display(Name = "Phosanis Nightmare")]
    PhosanisNightmare,
    Obor,
    Sarachnis,
    Scorpia,
    Skotizo,
    Tempoross,
    Gauntlet,
    [Display(Name = "Corrupted Gauntlet")]
    CorruptedGauntlet,
    [Display(Name = "Theatre of Blood")]
    TheatreofBlood,
    [Display(Name = "Theatre of Blood Hard Mode")]
    TheatreofBloodHardMode,
    [Display(Name = "Thermonuclear Smoke Devil")]
    ThermonuclearSmokeDevil,
    [Display(Name = "Tombs of Amascut")]
    TombsOfAmascut,
    [Display(Name = "Tombs of Amascut Expert Mode")]
    TombsOfAmascutExpertMode,
    Zuk,
    Jad,
    Venenatis,
    Vetion,
    Vorkath,
    Wintertodt,
    Zalcano,
    Zulrah
}
