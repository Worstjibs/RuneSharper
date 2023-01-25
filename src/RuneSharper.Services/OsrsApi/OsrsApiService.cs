using DotnetOsrsApiWrapper;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Stats;

public class OsrsApiService : IOsrsApiService
{
    private readonly IPlayerInfoService _playerInfoService;

    public OsrsApiService(IPlayerInfoService playerInfoService)
    {
        _playerInfoService = playerInfoService;
    }

    public async Task<Snapshot?> QueryHiScoresByAccountAsync(Character account)
    {
        var playerInfo = await _playerInfoService.GetPlayerInfoAsync(account.UserName);

        return PlayerInfoToSnapshot(playerInfo);
    }

    public async Task<Dictionary<Character, Snapshot?>> QueryHiScoresByAccountsAsync(IEnumerable<Character> accounts)
    {
        var playerInfos = await _playerInfoService.GetPlayerInfoAsync(accounts.Select(x => x.UserName).ToArray());

        return accounts
            .Join(
                playerInfos,
                c => c.UserName,
                p => p.Name,
                (c, p) => new { Character = c, Snapshot = PlayerInfoToSnapshot(p) })
            .ToDictionary(x => x.Character, x => x.Snapshot);
    }

    private static Snapshot? PlayerInfoToSnapshot(PlayerInfo playerInfo)
    {
        if (playerInfo.Status == PlayerInfoStatus.NotFound)
            return null;

        var snapshot = new Snapshot();

        snapshot.Skills = playerInfo.Skills().Select(x =>
        {
            _ = Enum.TryParse<SkillType>(x.Name, out var type);

            return new SkillSnapshot
            {
                Type = type,
                Experience = x.Experience,
                Level = x.Level,
                Rank = x.Rank
            };
        }).ToList();

        if (playerInfo.Minigames().Count() != Enum.GetValues<ActivityType>().Length)
            throw new FormatException("Mismatch between PlayerInfo and ActivityType Enum. Please review values in Enum.");

        snapshot.Activities = playerInfo.Minigames().Select(x =>
        {
            _ = Enum.TryParse<ActivityType>(x.Name, out var type);

            return new ActivitySnapshot
            {
                Rank = x.Rank,
                Score = x.Score,
                Type = type,
                Snapshot = snapshot
            };
        }).ToList();

        return snapshot;
    }
}
