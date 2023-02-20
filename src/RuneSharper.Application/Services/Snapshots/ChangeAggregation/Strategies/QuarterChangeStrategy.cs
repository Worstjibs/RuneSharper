using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

public class QuarterChangeStrategy : BaseChangeStrategy
{
    public QuarterChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override FrequencyType Frequency => FrequencyType.Quarter;
    protected override DateTime dateFrom => InstantiatedUtc.AddMonths(-3);
    protected override DateTime dateTo => InstantiatedUtc;
}
