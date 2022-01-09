using DotnetOsrsApiWrapper;
using RuneSharper.Data;
using RuneSharper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.Stats {
    public class StatsService : IStatsService {
        public IEnumerable<Skill> QueryStatsForUserId(string username) {
            return new PlayerInfo(username).Skills();
        }
    }
}
