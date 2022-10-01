using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Models;

public class CharacterListModel
{
    public string UserName { get; set; } = default!;
    public int TotalLevel { get; set; }
    public int TotalExperience { get; set; }
    public DateTime FirstTracked { get; set; } = default!;
}
