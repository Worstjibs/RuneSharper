using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Models;

public class CharacterViewModel
{
    public string UserName { get; set; } = default!;
    public DateTime FirstTracked { get; set; }
    public StatsModel? Stats { get; set; }
    public ActivitiesModel? Activities { get; set; }
    public List<KeyValuePair<Frequency, ActivitiesModel?>> ActivitiesChange { get; set; } = new();
}
