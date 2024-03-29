﻿using RuneSharper.Shared.Enums;
using RuneSharper.Application.Models;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation;

public class StatsChangeAggregationHandler : ChangeAggregationHandler<StatsChangeModel>
{
    private readonly ISnapshotsService _snapshotsService;

    public StatsChangeAggregationHandler(
        IEnumerable<IChangeAggregationStrategy> strategies,
        ISnapshotsService snapshotsService)
        : base(strategies)
    {
        _snapshotsService = snapshotsService;
    }

    public override async Task<StatsChangeModel> GetChangeAggregationForUser(string userName, FrequencyType frequency)
    {
        var strategy = GetStrategy(frequency);

        var statsModel = await _snapshotsService.GetStatsChangeForUser(userName, strategy.DateRange);

        return new StatsChangeModel(strategy.Frequency, strategy.DateRange, statsModel);
    }

    public override async Task<IEnumerable<StatsChangeModel>> GetChangeAggregationsForUser(string userName)
    {
        var list = new List<StatsChangeModel>();

        foreach (var strategy in _strategies)
        {
            var model = await _snapshotsService.GetStatsChangeForUser(userName, strategy.DateRange);
            list.Add(new StatsChangeModel(strategy.Frequency, strategy.DateRange, model));
        }

        return list;
    }
}
