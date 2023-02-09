using RuneSharper.Services.Services.DateTimeProvider;
using RuneSharper.Services.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public class YearChangeStrategy : BaseChangeStrategy
{
    public YearChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override Frequency Frequency => Frequency.Year;

    protected override DateTime dateFrom => InstantiatedUtc.AddYears(-1);
    protected override DateTime dateTo => InstantiatedUtc;
}
