using RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Services.Snapshots.ChangeAggregation;

public abstract class ChangeAggregationHandler<T> : IChangeAggregationHandler<T>
{
    protected readonly IEnumerable<IChangeAggregationStrategy> _strategies;

    protected ChangeAggregationHandler(IEnumerable<IChangeAggregationStrategy> strategies)
    {
        _strategies = strategies;
    }

    public abstract Task<T> GetChangeAggregationForUser(string userName, Frequency frequency);

    public abstract Task<IEnumerable<T>> GetChangeAggregationsForUser(string userName);

    public IChangeAggregationStrategy GetStrategy(Frequency frequency)
    {
        return _strategies.First(x => x.Frequency == frequency);
    }
}
