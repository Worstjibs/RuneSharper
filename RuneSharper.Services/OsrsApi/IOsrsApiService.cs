using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Services.Stats {
    public interface IOsrsApiService {
        Task<Snapshot> QueryHiScoresByAccount(Character account); 
    }
}
