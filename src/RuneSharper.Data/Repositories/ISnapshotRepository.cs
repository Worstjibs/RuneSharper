using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories;

public interface ISnapshotRepository : IRepository<Snapshot>
{
    public Task<Snapshot?> GetLatestSnapshotAsync(string userName);
    public Task<IEnumerable<Snapshot>> GetLatestSnapshotsAsync(IEnumerable<string> userNames);
    public Task<Snapshot?> GetFirstSnapshotAsync(string userName, DateRange dateRange);
    public Task<Snapshot?> GetLastSnapshotAsync(string userName, DateRange dateRange);
}
