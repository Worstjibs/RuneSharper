﻿using RuneSharper.Domain.Helpers;
using RuneSharper.Services.Models;

namespace RuneSharper.Services.Snapshots;
public interface ISnapshotsService
{
    Task<StatsModel?> GetStatsChangeForUser(string userName, DateRange dateRange);
    Task<ActivitiesModel?> GetActivitesChangeForUser(string userName, DateRange dateRange);
}