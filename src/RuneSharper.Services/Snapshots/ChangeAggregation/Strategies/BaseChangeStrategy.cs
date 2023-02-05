using RuneSharper.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.Snapshots.ChangeAggregation.Strategies;

public abstract class BaseChangeStrategy : IChangeAggregationStrategy
{
    private readonly IDateTimeProvider _dateTimeProvider;

    protected BaseChangeStrategy(IDateTimeProvider dateTimeProvider) => _dateTimeProvider = dateTimeProvider;

    protected DateTime InstantiatedUtc => _dateTimeProvider.InstantiatedUtc;

    protected abstract DateTime dateFrom { get; }
    protected abstract DateTime dateTo { get; }

    public abstract Frequency Frequency { get; }

    public DateRange DateRange => new DateRange(dateFrom, dateTo);
}
