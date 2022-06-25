using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Shared.Entities;

public class Character : BaseIntEntity {
    public string UserName { get; set; } = default!;
    public ICollection<Snapshot> Snapshots { get; set; } = default!;
}
