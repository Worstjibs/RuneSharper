using System.ComponentModel.DataAnnotations.Schema;

namespace RuneSharper.Domain.Entities.Snapshots;

public class SnapshotEntity<T> : BaseIntEntity where T : Enum
{
    public T Type { get; set; } = default!;
    public int Rank { get; set; }
    public Snapshot Snapshot { get; set; } = default!;
}
