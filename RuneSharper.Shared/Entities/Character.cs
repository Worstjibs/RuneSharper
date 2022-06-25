using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Shared.Entities;

public class Character : BaseIntEntity, IDateCreated
{
    public string UserName { get; set; } = default!;
    public ICollection<Snapshot> Snapshots { get; set; } = default!;
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
}
