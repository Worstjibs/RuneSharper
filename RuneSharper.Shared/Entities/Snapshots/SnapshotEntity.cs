using System.ComponentModel.DataAnnotations.Schema;

namespace RuneSharper.Shared.Entities.Snapshots;

public class SnapshotEntity<T> : BaseIntEntity where T : Enum
{
    public T Type { get; set; } = default!;
    public int Rank { get; set; }
    public Snapshot Snapshot { get; set; } = default!;
    [NotMapped]
    public override DateTime DateCreated { get => Snapshot.DateCreated; set => throw new Exception("DateCreated should be set at Snapshot level"); }
}
