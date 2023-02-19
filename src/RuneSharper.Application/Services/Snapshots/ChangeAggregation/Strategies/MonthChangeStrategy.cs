using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

public class MonthChangeStrategy : BaseChangeStrategy
{
    public MonthChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override FrequencyType Frequency => FrequencyType.Month;

    protected override DateTime dateFrom => InstantiatedUtc.AddMonths(-1);
    protected override DateTime dateTo => InstantiatedUtc;
}
