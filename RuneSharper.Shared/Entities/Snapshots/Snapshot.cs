namespace RuneSharper.Shared.Entities.Snapshots
{
    public record Snapshot : BaseIntEntity {
        public Character Character { get; set; } = default!;
        public ICollection<SkillSnapshot> Skills { get; set; } = default!;
        public ICollection<ActivitySnapshot> Activities { get; set; } = default!;
    }
}
