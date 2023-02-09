using RuneSharper.Domain.Enums;

namespace RuneSharper.Domain.Entities.Snapshots;

public class ActivitySnapshot : SnapshotEntity<ActivityType>
{
    public int Score { get; set; }
}
