using RuneSharper.Domain.Enums;
using RuneSharper.Shared.Helpers;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots;
public interface ISnapshotsService
{
    Task<StatsModel?> GetStatsChangeForUser(string userName, DateRange dateRange);
    Task<ActivitiesModel?> GetActivitesChangeForUser(string userName, DateRange dateRange);
}