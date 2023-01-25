using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public class YearActivitiesChangeStrategy : IChangeAggregationStrategy
{
    public Frequency Frequency => Frequency.Year;
    public DateRange DateRange => new DateRange(DateTime.UtcNow.AddYears(-1), DateTime.UtcNow);
}
