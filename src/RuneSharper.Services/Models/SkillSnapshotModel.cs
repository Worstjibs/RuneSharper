namespace RuneSharper.Services.Models;

public class SkillSnapshotModel
{
    public string Type { get; set; } = default!;
    public int Experience { get; set; }
    public int Level { get; set; }
    public int Rank { get; set; }
    public DateTime DateCreated { get; set; }
}
