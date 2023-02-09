using RuneSharper.Shared.Enums;
using RuneSharper.Services.Models;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;

namespace RuneSharper.Services.Services.Snapshots.ChangeAggregation;

public class ActivitiesChangeAggregationHandler : ChangeAggregationHandler<ActivitiesChangeModel>
{
    private readonly ISnapshotsService _snapshotsService;

    public ActivitiesChangeAggregationHandler(
        IEnumerable<IChangeAggregationStrategy> strategies,
        ISnapshotsService snapshotsService)
        : base(strategies)
    {
        _snapshotsService = snapshotsService;
    }

    public override async Task<ActivitiesChangeModel> GetChangeAggregationForUser(string userName, Frequency frequency)
    {
        var strategy = GetStrategy(frequency);

        var model = await _snapshotsService.GetActivitesChangeForUser(userName, strategy.DateRange);

        return new ActivitiesChangeModel(frequency, strategy.DateRange, model);
    }

    public override async Task<IEnumerable<ActivitiesChangeModel>> GetChangeAggregationsForUser(string userName)
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
