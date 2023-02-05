using RuneSharper.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public class DayChangeStrategy : BaseChangeStrategy
{
    public DayChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override Frequency Frequency => Frequency.Day;

    protected override DateTime dateFrom => InstantiatedUtc.AddDays(-1);
    protected override DateTime dateTo => InstantiatedUtc;
}
