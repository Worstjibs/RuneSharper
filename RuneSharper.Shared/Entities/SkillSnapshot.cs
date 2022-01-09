using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Entities {
    public record SkillSnapshot : BaseIntEntity {
        public int Rank { get; set; }
        public int Level { get; set; }
        public int Experience { get; set; }
        public SkillType SkillType { get; set; }
        public Snapshot Snapshot { get; set; } = default!;
    }
}
