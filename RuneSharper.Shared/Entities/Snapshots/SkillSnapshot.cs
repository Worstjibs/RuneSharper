using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Entities.Snapshots {
    public record SkillSnapshot : SnapshotEntity<SkillType> {
        public int Level { get; set; }
        public int Experience { get; set; }
    }
}
