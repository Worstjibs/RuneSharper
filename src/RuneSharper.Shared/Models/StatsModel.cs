#nullable disable
using RuneSharper.Domain.Enums;
using RuneSharper.Shared.Attributes;
using RuneSharper.Domain.Entities.Snapshots;
using System.Reflection;

namespace RuneSharper.Shared.Models;

public class StatsModel
{
    private readonly PropertyInfo[] _properties;

    public StatsModel() { }

    public StatsModel(Snapshot snapshot)
    {
        _properties = GetType().GetProperties();

        foreach (var skill in snapshot.Skills)
        {
            SetProperty(skill);
        }
    }

    public StatsModel(IEnumerable<SkillSnapshot> skillSnapshots)
    {
        _properties = GetType().GetProperties();

        foreach (var skill in skillSnapshots)
        {
            SetProperty(skill);
        }
    }

    [SkillType(SkillType.Overall)]
    public SkillModel Overall { get; set; }

    [SkillType(SkillType.Attack)]
    public SkillModel Attack { get; set; }

    [SkillType(SkillType.Defence)]
    public SkillModel Defence { get; set; }

    [SkillType(SkillType.Strength)]
    public SkillModel Strength { get; set; }

    [SkillType(SkillType.Hitpoints)]
    public SkillModel Hitpoints { get; set; }

    [SkillType(SkillType.Ranged)]
    public SkillModel Ranged { get; set; }

    [SkillType(SkillType.Prayer)]
    public SkillModel Prayer { get; set; }

    [SkillType(SkillType.Magic)]
    public SkillModel Magic { get; set; }

    [SkillType(SkillType.Cooking)]
    public SkillModel Cooking { get; set; }

    [SkillType(SkillType.Woodcutting)]
    public SkillModel Woodcutting { get; set; }

    [SkillType(SkillType.Fletching)]
    public SkillModel Fletching { get; set; }

    [SkillType(SkillType.Fishing)]
    public SkillModel Fishing { get; set; }

    [SkillType(SkillType.Firemaking)]
    public SkillModel Firemaking { get; set; }

    [SkillType(SkillType.Crafting)]
    public SkillModel Crafting { get; set; }

    [SkillType(SkillType.Smithing)]
    public SkillModel Smithing { get; set; }

    [SkillType(SkillType.Mining)]
    public SkillModel Mining { get; set; }

    [SkillType(SkillType.Herblore)]
    public SkillModel Herblore { get; set; }

    [SkillType(SkillType.Agility)]
    public SkillModel Agility { get; set; }

    [SkillType(SkillType.Thieving)]
    public SkillModel Thieving { get; set; }

    [SkillType(SkillType.Slayer)]
    public SkillModel Slayer { get; set; }

    [SkillType(SkillType.Farming)]
    public SkillModel Farming { get; set; }

    [SkillType(SkillType.Runecrafting)]
    public SkillModel Runecrafting { get; set; }

    [SkillType(SkillType.Hunter)]
    public SkillModel Hunter { get; set; }

    [SkillType(SkillType.Construction)]
    public SkillModel Construction { get; set; }

    private void SetProperty(SkillSnapshot skillSnapshot)
    {
        var model = typeof(StatsModel);
        var property = _properties.First(x => x.GetCustomAttribute<SkillTypeAttribute>()?.Type == skillSnapshot.Type);

        var skillModel = new SkillModel
        {
            Name = skillSnapshot.Type.ToString(),
            Experience = skillSnapshot.Experience,
            Level = skillSnapshot.Level,
            Rank = skillSnapshot.Rank
        };

        property.SetValue(this, skillModel);
    }
}
