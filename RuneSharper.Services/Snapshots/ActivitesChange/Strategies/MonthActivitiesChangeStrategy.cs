using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ActivitesChange.Strategies;

public class MonthActivitiesChangeStrategy : IActivitiesChangeAggregationStrategy
{
    public Frequency Frequency => Frequency.Month;
    public DateRange DateRange => new DateRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
}
