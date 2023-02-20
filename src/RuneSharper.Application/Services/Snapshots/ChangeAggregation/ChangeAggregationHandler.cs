using RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation;

public abstract class ChangeAggregationHandler<T> : IChangeAggregationHandler<T>
{
    protected readonly IEnumerable<IChangeAggregationStrategy> _strategies;

    protected ChangeAggregationHandler(IEnumerable<IChangeAggregationStrategy> strategies)
    {
        _strategies = strategies.OrderBy(x => x.Frequency);
    }

    public abstract Task<T> GetChangeAggregationForUser(string userName, FrequencyType frequency);

    public abstract Task<IEnumerable<T>> GetChangeAggregationsForUser(string userName);

    public IChangeAggregationStrategy GetStrategy(FrequencyType frequency)
    {
        return _strategies.First(x => x.Frequency == frequency);
    }
}
