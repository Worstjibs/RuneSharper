using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Snapshots.ChangeAggregation;

public interface IChangeAggregationHandler<T>
{
    Task<T> GetChangeAggregationForUser(string userName, Frequency frequency);
    Task<IEnumerable<T>> GetChangeAggregationsForUser(string userName);
}
