using RuneSharper.Data.Repositories;
using RuneSharper.Services.Snapshots.ActivitesChange.Strategies;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots.ActivitesChange;

public class ActivitiesChangeAggregationHandler : IActivitiesChangeAggregationHandler
{
    private readonly IEnumerable<IActivitiesChangeAggregationStrategy> _strategies;
    private readonly ISnapshotsService _snapshotsService;

    public ActivitiesChangeAggregationHandler(
        IEnumerable<IActivitiesChangeAggregationStrategy> strategies,
        ISnapshotsService snapshotsService)
    {
        _strategies = strategies;
        _snapshotsService = snapshotsService;
    }

    public async Task<ActivitiesModel?> GetActivitiesChangeAggregationForUser(string userName, Frequency frequency)
    {
        var strategy = _strategies.First(x => x.Frequency == frequency);

        return await _snapshotsService.GetActivitesChangeForUser(userName, strategy.DateRange);
    }

    public async Task<Dictionary<Frequency, ActivitiesModel?>> GetActivitiesChangeAggregationsForUser(string userName)
    {
        var dictionary = new Dictionary<Frequency, ActivitiesModel?>();

        foreach (var strategy in _strategies)
        {
            dictionary.Add(
                strategy.Frequency,
                await _snapshotsService.GetActivitesChangeForUser(userName, strategy.DateRange));
        }

        return dictionary;
    }
}
