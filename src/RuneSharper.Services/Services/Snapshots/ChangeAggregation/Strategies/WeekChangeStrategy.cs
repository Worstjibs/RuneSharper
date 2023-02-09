using RuneSharper.Services.Services.DateTimeProvider;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public class WeekChangeStrategy : BaseChangeStrategy
{
    public WeekChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override Frequency Frequency => Frequency.Week;

    protected override DateTime dateFrom => InstantiatedUtc.AddDays(-7);
    protected override DateTime dateTo => InstantiatedUtc;
}
