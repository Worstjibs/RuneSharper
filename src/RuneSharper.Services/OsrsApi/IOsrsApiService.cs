﻿using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Entities.Snapshots;

namespace RuneSharper.Services.Stats;

public interface IOsrsApiService {
    Task<Snapshot?> QueryHiScoresByAccountAsync(Character account);
    Task<Dictionary<Character, Snapshot?>> QueryHiScoresByAccountsAsync(IEnumerable<Character> accounts);
}
