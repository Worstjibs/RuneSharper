using DotnetOsrsApiWrapper;
using RuneSharper.Data;
using RuneSharper.Shared;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.Stats
{
    public class OsrsApiService : IOsrsApiService
    {
        public Snapshot QueryHiScoresByAccount(Character account)
        {
            var playerInfo = new PlayerInfo(account.UserName);

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
