using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ActivitesChange.Strategies;

public class WeekActivitiesChangeStrategy : IActivitiesChangeAggregationStrategy
{
    public Frequency Frequency => Frequency.Week;
    public DateRange DateRange => new DateRange(DateTime.UtcNow.AddDays(-7), DateTime.UtcNow);
}
