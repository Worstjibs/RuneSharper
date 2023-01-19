using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public class MonthActivitiesChangeStrategy : IChangeAggregationStrategy
{
    public Frequency Frequency => Frequency.Month;
    public DateRange DateRange => new DateRange(DateTime.UtcNow.AddMonths(-1), DateTime.UtcNow);
}
