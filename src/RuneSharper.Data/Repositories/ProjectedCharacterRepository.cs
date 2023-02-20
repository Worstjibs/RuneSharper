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
            .SelectMany(c => c.Snapshots
                .OrderByDescending(s => s.DateCreated)
                .Take(1)
                .SelectMany(s => s.Skills.Where(skill => skill.Type == SkillType.Overall)))
            .Select(x => new CharacterListModel
            {
                UserName = x.Snapshot.Character.UserName,
                FirstTracked = x.Snapshot.Character.DateCreated,
                TotalLevel = x.Level,
                TotalExperience = x.Experience
            });

        if (orderBy is not null)
            query = direction == SortDirection.Descending 
                ? query.OrderByDescending(orderBy) 
                : query.OrderBy(orderBy);

        return await query.ToListAsync();
    }
}
