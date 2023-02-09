using RuneSharper.Domain.Enums;

namespace RuneSharper.Domain.Entities.Snapshots;

public class SkillSnapshot : SnapshotEntity<SkillType>
{
    public int Level { get; set; }
    public int Experience { get; set; }
}
