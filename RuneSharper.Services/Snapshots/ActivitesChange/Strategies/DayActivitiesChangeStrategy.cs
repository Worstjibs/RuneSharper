using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ActivitesChange.Strategies;

public class DayActivitiesChangeStrategy : IActivitiesChangeAggregationStrategy
{
    public Frequency Frequency => Frequency.Day;
    public DateRange DateRange => new DateRange(DateTime.UtcNow.AddDays(-1), DateTime.UtcNow);
}
