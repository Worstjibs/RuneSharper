namespace RuneSharper.Shared.Entities.Snapshots;

public class Snapshot : BaseIntEntity, IDateCreated
{
    public Character Character { get; set; } = default!;
    public ICollection<SkillSnapshot> Skills { get; set; } = default!;
    public ICollection<ActivitySnapshot> Activities { get; set; } = default!;
    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
}
