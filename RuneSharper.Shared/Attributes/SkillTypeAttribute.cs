using RuneSharper.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Attributes;

public class SkillTypeAttribute : Attribute
{
    public SkillType Type { get; set; }

    public SkillTypeAttribute(SkillType type)
    {
        Type = type;
    }
}
