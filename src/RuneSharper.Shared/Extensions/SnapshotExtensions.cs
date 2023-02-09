using RuneSharper.Domain.Enums;

namespace RuneSharper.Shared.Extensions;

public static class SnapshotExtensions
{
    private static readonly ActivityType[] _clueTypes = new[] {
        ActivityType.TotalCluesScrolls,
        ActivityType.BeginnerClueScrolls,
        ActivityType.EasyClueScrolls,
        ActivityType.MediumClueScrolls,
        ActivityType.HardClueScrolls,
        ActivityType.EliteClueScrolls,
        ActivityType.MasterClueScrolls
    };

    private static readonly ActivityType[] _other = new[]
    {
        ActivityType.LeaguePoints,
        ActivityType.BountyHunterRogues,
        ActivityType.BountyHunter,
        ActivityType.LastManStanding,
        ActivityType.PvPArena
    };

    public static bool IsClue(this ActivityType type)
    {
        return _clueTypes.Contains(type);
    }

    public static bool IsOther(this ActivityType type)
    {
        return _other.Contains(type);
    }

    public static bool IsBoss(this ActivityType type)
    {
        return !_clueTypes.Contains(type) && !_other.Contains(type);
    }
}
