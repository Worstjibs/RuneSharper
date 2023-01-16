using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots.ActivitesChange;

public interface IActivitiesChangeAggregationHandler
{
    Task<ActivitiesChangeModel> GetActivitiesChangeAggregationForUser(string userName, Frequency frequency);
    Task<IEnumerable<ActivitiesChangeModel>> GetActivitiesChangeAggregationsForUser(string userName);
}
