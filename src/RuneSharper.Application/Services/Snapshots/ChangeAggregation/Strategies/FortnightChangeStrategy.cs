using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

public class FortnightChangeStrategy : BaseChangeStrategy
{
    public FortnightChangeStrategy(IDateTimeProvider dateTimeProvider) : base(dateTimeProvider) { }

    public override FrequencyType Frequency => FrequencyType.Fortnight;

    protected override DateTime dateFrom => InstantiatedUtc.AddDays(-14);
    protected override DateTime dateTo => InstantiatedUtc;
}
