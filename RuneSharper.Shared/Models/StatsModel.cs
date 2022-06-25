using RuneSharper.Shared.Attributes;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Models;

public class StatsModel
{
    [SkillType(Enums.SkillType.Overall)]
    public int Overall { get; set; }

    [SkillType(Enums.SkillType.Attack)]
    public int Attack { get; set; }

    [SkillType(Enums.SkillType.Defence)]
    public int Defence { get; set; }

    [SkillType(Enums.SkillType.Strength)]
    public int Strength { get; set; }

    [SkillType(Enums.SkillType.Hitpoints)]
    public int Hitpoints { get; set; }

    [SkillType(Enums.SkillType.Ranged)]
    public int Ranged { get; set; }

    [SkillType(Enums.SkillType.Prayer)]
    public int Prayer { get; set; }

    [SkillType(Enums.SkillType.Magic)]
    public int Magic { get; set; }

    [SkillType(Enums.SkillType.Cooking)]
    public int Cooking { get; set; }

    [SkillType(Enums.SkillType.Woodcutting)]
    public int Woodcutting { get; set; }

    [SkillType(Enums.SkillType.Fletching)]
    public int Fletching { get; set; }

    [SkillType(Enums.SkillType.Fishing)]
    public int Fishing { get; set; }

    [SkillType(Enums.SkillType.Firemaking)]
    public int Firemaking { get; set; }

    [SkillType(Enums.SkillType.Crafting)]
    public int Crafting { get; set; }

    [SkillType(Enums.SkillType.Smithing)]
    public int Smithing { get; set; }

    [SkillType(Enums.SkillType.Mining)]
    public int Mining { get; set; }

    [SkillType(Enums.SkillType.Herblore)]
    public int Herblore { get; set; }

    [SkillType(Enums.SkillType.Agility)]
    public int Agility { get; set; }


    [SkillType(Enums.SkillType.Thieving)]
    public int Thieving { get; set; }

    [SkillType(Enums.SkillType.Slayer)]
    public int Slayer { get; set; }

    [SkillType(Enums.SkillType.Farming)]
    public int Farming { get; set; }

    [SkillType(Enums.SkillType.Runecrafting)]
    public int Runecrafting { get; set; }

    [SkillType(Enums.SkillType.Hunter)]
    public int Hunter { get; set; }

    [SkillType(Enums.SkillType.Construction)]
    public int Construction { get; set; }

    public StatsModel(Snapshot snapshot)
    {
        foreach (var skill in snapshot.Skills)
        {
            SetProperty(skill);
        }
    }

    private void SetProperty(SkillSnapshot skillSnapshot)
    {
        var model = typeof(StatsModel);
        var property = model.GetProperties().First(x => x.GetCustomAttribute<SkillTypeAttribute>()?.Type == skillSnapshot.Type);

        property.SetValue(this, skillSnapshot.Level);
    }
}
