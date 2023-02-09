using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Shared.Extensions;

namespace RuneSharper.Services.Models;

public class ActivitiesModel
{
    public ActivitiesModel() { }

    public ActivitiesModel(IEnumerable<ActivitySnapshot> activities)
    {
        foreach (var activity in activities)
        {
            var model = new ActivityModel
            {
                Score = activity.Score,
                Name = activity.Type.GetDisplayName(),
                Rank = activity.Rank
            };

            if (activity.Type.IsBoss())
                Bosses.Add(model);

            if (activity.Type.IsClue())
                Clues.Add(model);

            if (activity.Type.IsOther())
                Other.Add(model);
        }
    }

    public ICollection<ActivityModel> Clues { get; set; } = new List<ActivityModel>();
    public ICollection<ActivityModel> Bosses { get; set; } = new List<ActivityModel>();
    public ICollection<ActivityModel> Other { get; set; } = new List<ActivityModel>();
}
