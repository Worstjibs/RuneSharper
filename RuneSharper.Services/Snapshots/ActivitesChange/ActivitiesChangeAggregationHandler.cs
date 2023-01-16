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

    public async Task<ActivitiesChangeModel> GetActivitiesChangeAggregationForUser(string userName, Frequency frequency)
    {
        var strategy = _strategies.First(x => x.Frequency == frequency);

        var model = await _snapshotsService.GetActivitesChangeForUser(userName, strategy.DateRange);

        return new ActivitiesChangeModel(frequency, strategy.DateRange, model);
    }

    public async Task<IEnumerable<ActivitiesChangeModel>> GetActivitiesChangeAggregationsForUser(string userName)
    {
        var list = new List<ActivitiesChangeModel>();

        foreach (var strategy in _strategies)
        {
            var model = await _snapshotsService.GetActivitesChangeForUser(userName, strategy.DateRange);
            list.Add(new ActivitiesChangeModel(strategy.Frequency, strategy.DateRange, model));
        }

        return list;
    }
}
