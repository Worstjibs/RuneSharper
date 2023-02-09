using RuneSharper.Domain.Helpers;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;

public interface IChangeAggregationStrategy
{
    Frequency Frequency { get; }
    DateRange DateRange { get; }
}
