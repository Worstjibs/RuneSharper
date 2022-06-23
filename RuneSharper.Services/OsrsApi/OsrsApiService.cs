using DotnetOsrsApiWrapper;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Stats
{
    public class OsrsApiService : IOsrsApiService
    {
        private readonly IPlayerInfoService _playerInfoService;

        public OsrsApiService(IPlayerInfoService playerInfoService)
        {
            _playerInfoService = playerInfoService;
        }

        public async Task<Snapshot> QueryHiScoresByAccount(Character account)
        {
            var playerInfo = await _playerInfoService.GetPlayerInfoAsync(account.UserName);

            var snapshot = new Snapshot { Character = account };

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
}
