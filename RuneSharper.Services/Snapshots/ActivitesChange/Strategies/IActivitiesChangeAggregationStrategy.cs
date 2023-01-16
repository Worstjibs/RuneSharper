using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ActivitesChange.Strategies;

public interface IActivitiesChangeAggregationStrategy
{
    Frequency Frequency { get; }
    DateRange DateRange { get; }
}
