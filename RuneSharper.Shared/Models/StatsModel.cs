using RuneSharper.Shared.Attributes;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;
using System.Reflection;

namespace RuneSharper.Shared.Models;

public class StatsModel
{
    [SkillType(SkillType.Overall)]
    public int Overall { get; set; }

    [SkillType(SkillType.Attack)]
    public int Attack { get; set; }

    [SkillType(SkillType.Defence)]
    public int Defence { get; set; }

    [SkillType(SkillType.Strength)]
    public int Strength { get; set; }

    [SkillType(SkillType.Hitpoints)]
    public int Hitpoints { get; set; }

    [SkillType(SkillType.Ranged)]
    public int Ranged { get; set; }

    [SkillType(SkillType.Prayer)]
    public int Prayer { get; set; }

    [SkillType(SkillType.Magic)]
    public int Magic { get; set; }

    [SkillType(SkillType.Cooking)]
    public int Cooking { get; set; }

    [SkillType(SkillType.Woodcutting)]
    public int Woodcutting { get; set; }

    [SkillType(SkillType.Fletching)]
    public int Fletching { get; set; }

    [SkillType(SkillType.Fishing)]
    public int Fishing { get; set; }

    [SkillType(SkillType.Firemaking)]
    public int Firemaking { get; set; }

    [SkillType(SkillType.Crafting)]
    public int Crafting { get; set; }

    [SkillType(SkillType.Smithing)]
    public int Smithing { get; set; }

    [SkillType(SkillType.Mining)]
    public int Mining { get; set; }

    [SkillType(SkillType.Herblore)]
    public int Herblore { get; set; }

    [SkillType(SkillType.Agility)]
    public int Agility { get; set; }

    [SkillType(SkillType.Thieving)]
    public int Thieving { get; set; }

    [SkillType(SkillType.Slayer)]
    public int Slayer { get; set; }

    [SkillType(SkillType.Farming)]
    public int Farming { get; set; }

    [SkillType(SkillType.Runecrafting)]
    public int Runecrafting { get; set; }

    [SkillType(SkillType.Hunter)]
    public int Hunter { get; set; }

    [SkillType(SkillType.Construction)]
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
