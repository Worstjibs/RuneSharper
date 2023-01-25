using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public interface IChangeAggregationStrategy
{
    Frequency Frequency { get; }
    DateRange DateRange { get; }
}
