namespace RuneSharper.Shared.Entities {
    public record Snapshot : BaseIntEntity {
        public Account Account { get; set; } = default!;
        public ICollection<SkillSnapshot> Skills { get; set; } = default!;
    }
}
