using DotnetOsrsApiWrapper;
using RuneSharper.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.Stats {
    public interface IStatsService {
        IEnumerable<Skill> QueryStatsForUserId(string userId); 
    }
}
