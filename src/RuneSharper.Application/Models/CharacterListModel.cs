using RuneSharper.Application.Attributes;

namespace RuneSharper.Application.Models;

public class CharacterListModel
{
    [SortMapping("Characters", "Username")]
    public string UserName { get; set; } = default!;

    [SortMapping("SkillSnapshot", "Level")]
    public int TotalLevel { get; set; }

    [SortMapping("SkillSnapshot", "Experience")]
    public int TotalExperience { get; set; }

    [SortMapping("Characters", "DateCreated")]
    public DateTime FirstTracked { get; set; }
}
