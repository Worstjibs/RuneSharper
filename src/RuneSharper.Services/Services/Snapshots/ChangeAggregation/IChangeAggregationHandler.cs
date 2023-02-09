using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Services.Snapshots.ChangeAggregation;

public interface IChangeAggregationHandler<T>
{
    Task<T> GetChangeAggregationForUser(string userName, Frequency frequency);
    Task<IEnumerable<T>> GetChangeAggregationsForUser(string userName);
}
