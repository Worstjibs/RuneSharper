using DotnetOsrsApiWrapper;
using RuneSharper.Shared;
using RuneSharper.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.Stats {
    public interface IOsrsApiService {
        Snapshot QueryHiScoresByAccount(Character account); 
    }
}
