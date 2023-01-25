using RuneSharper.Shared.Enums;

namespace RuneSharper.Shared.Attributes;

public class SkillTypeAttribute : Attribute
{
    public SkillType Type { get; set; }

    public SkillTypeAttribute(SkillType type)
    {
        Type = type;
    }
}
