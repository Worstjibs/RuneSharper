using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Models;

public class CharacterViewModel
{
    public string UserName { get; set; } = string.Empty;
    public DateTime FirstTracked { get; set; }
    public StatsModel? Stats { get; set; }
    public ActivitiesModel? Activities { get; set; }
    public IEnumerable<ActivitiesChangeModel> ActivitiesChange { get; set; } = Enumerable.Empty<ActivitiesChangeModel>();
    public IEnumerable<StatsChangeModel> StatsChange { get; set; } = Enumerable.Empty<StatsChangeModel>();
}
