﻿using RuneSharper.Domain.Helpers;
using RuneSharper.Application.Services.DateTimeProvider;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

public abstract class BaseChangeStrategy : IChangeAggregationStrategy
{
    private readonly IDateTimeProvider _dateTimeProvider;

    protected BaseChangeStrategy(IDateTimeProvider dateTimeProvider) => _dateTimeProvider = dateTimeProvider;

    protected DateTime InstantiatedUtc => _dateTimeProvider.InstantiatedUtc;

    protected abstract DateTime dateFrom { get; }
    protected abstract DateTime dateTo { get; }

    public abstract FrequencyType Frequency { get; }

    public DateRange DateRange => new DateRange(dateFrom, dateTo);
}
