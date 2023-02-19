using RuneSharper.Domain.Helpers;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Application.Services.Snapshots.ChangeAggregation.Strategies;

public interface IChangeAggregationStrategy
{
    FrequencyType Frequency { get; }
    DateRange DateRange { get; }
}
