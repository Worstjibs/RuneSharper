using RuneSharper.Shared.Helpers;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots;
public interface ISnapshotsService
{
    Task<StatsModel?> GetSnapshotChangeForUser(string userName, DateRange dateRange);
}