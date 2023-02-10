using RuneSharper.Domain.Helpers;
using RuneSharper.Application.Models;

namespace RuneSharper.Application.Services.Snapshots;
public interface ISnapshotsService
{
    Task<StatsModel?> GetStatsChangeForUser(string userName, DateRange dateRange);
    Task<ActivitiesModel?> GetActivitesChangeForUser(string userName, DateRange dateRange);
}