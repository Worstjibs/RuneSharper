﻿using RuneSharper.Domain.Entities.NgxCharts;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.Application.Services.LineCharts;

public interface ILineChartService
{
    Task<IEnumerable<LineChartModels>> GetSkillSnapshotData(string username, DateRange dateRange, bool includeOverall);
}