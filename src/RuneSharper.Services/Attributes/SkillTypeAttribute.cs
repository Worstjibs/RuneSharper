using RuneSharper.Domain.Enums;

namespace RuneSharper.Services.Attributes;

public class SkillTypeAttribute : Attribute
{
    public SkillType Type { get; set; }

    public SkillTypeAttribute(SkillType type)
    {
        Type = type;
    }
}
