using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Snapshots.ChangeAggregation.Strategies;

public class YearChangeStrategy : BaseChangeStrategy
{
    public YearChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override FrequencyType Frequency => FrequencyType.Year;

    protected override DateTime dateFrom => InstantiatedUtc.AddYears(-1);
    protected override DateTime dateTo => InstantiatedUtc;
}
