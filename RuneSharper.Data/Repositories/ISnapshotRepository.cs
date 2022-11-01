using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Data.Repositories;

public interface ISnapshotRepository : IRepository<Snapshot>
{
    public Task<Snapshot?> GetLatestSnapshotAsync(string userName);
    public Task<Dictionary<string, Snapshot>> GetLatestSnapshotByCharacter(IEnumerable<string> userNames);
}
