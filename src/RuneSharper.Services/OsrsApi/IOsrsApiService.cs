using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Services.Stats;

public interface IOsrsApiService {
    Task<Snapshot?> QueryHiScoresByAccountAsync(Character account);
    Task<Dictionary<Character, Snapshot?>> QueryHiScoresByAccountsAsync(IEnumerable<Character> accounts);
}
