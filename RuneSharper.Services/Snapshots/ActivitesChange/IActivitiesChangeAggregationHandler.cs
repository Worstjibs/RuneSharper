using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots.ActivitesChange;

public interface IActivitiesChangeAggregationHandler
{
    Task<ActivitiesModel?> GetActivitiesChangeAggregationForUser(string userName, Frequency frequency);
    Task<Dictionary<Frequency, ActivitiesModel?>> GetActivitiesChangeAggregationsForUser(string userName);
}
