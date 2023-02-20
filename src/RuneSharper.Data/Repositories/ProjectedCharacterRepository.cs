using Microsoft.EntityFrameworkCore;
using RuneSharper.Application.Models;
using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Enums;
using System.Linq.Expressions;

namespace RuneSharper.Data.Repositories;

public class ProjectedCharacterRepository : Repository<Character>, IProjectedCharacterRepository<CharacterListModel>
{
    public ProjectedCharacterRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<IEnumerable<CharacterListModel>> GetProjectedCharacters(Expression<Func<CharacterListModel, object>>? orderBy, SortDirection? direction)
    {
        var query = DbSet
            .Where(x => x.UserName != "becky")
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

        if (orderBy is not null)
            query = direction == SortDirection.Descending 
                ? query.OrderByDescending(orderBy) 
                : query.OrderBy(orderBy);

        return await query.ToListAsync();
    }
}
