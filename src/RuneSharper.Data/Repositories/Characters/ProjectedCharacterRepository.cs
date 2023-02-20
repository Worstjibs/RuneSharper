using Microsoft.EntityFrameworkCore;
using RuneSharper.Application.Attributes;
using RuneSharper.Application.Models;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Enums;
using System.Linq.Expressions;
using System.Reflection;

namespace RuneSharper.Data.Repositories.Characters;

public class ProjectedCharacterRepository : CharacterRepository, IProjectedCharacterRepository<CharacterListModel>
{
    public ProjectedCharacterRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CharacterListModel>> GetProjectedCharacters(string? orderBy, SortDirection? direction)
    {
        var orderExpression = GetProperty(orderBy);

        var query = DbSet
            .SelectMany(c => c.Snapshots
                .OrderByDescending(s => s.DateCreated)
                .Take(1)
                .SelectMany(s => s.Skills))
            .GroupBy(x => x.Snapshot.Character)
            .Select(x => new CharacterListModel
            {
                UserName = x.Key.UserName,
                FirstTracked = x.Key.DateCreated,
                TotalLevel = x.Max(s => s.Level),
                TotalExperience = x.Max(s => s.Experience),
                HighestSkill = x.Where(s => s.Type != SkillType.Overall).OrderByDescending(s => s.Experience).First().Type.ToString()
            });

        if (orderExpression is not null)
            query = direction == SortDirection.Descending
                ? query.OrderByDescending(orderExpression)
                : query.OrderBy(orderExpression);

        return await query.ToListAsync();
    }

    private static Expression<Func<CharacterListModel, object>>? GetProperty(string? sort)
    {
        if (sort is null)
            return null;

        var property = typeof(CharacterListModel)
            .GetProperty(sort, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.IgnoreCase);

        if (property == null)
            throw new ArgumentException($"Sort expression {sort} is invalid");

        if (property.GetCustomAttribute<UnsortableAttribute>() is not null)
            throw new ArgumentException($"Property {property.Name} is unsortable");

        var parameter = Expression.Parameter(typeof(CharacterListModel));
        var propertyExpression = Expression.Property(parameter, property);
        var conversion = Expression.Convert(propertyExpression, typeof(object));
        return Expression.Lambda<Func<CharacterListModel, object>>(conversion, parameter);
    }
}
