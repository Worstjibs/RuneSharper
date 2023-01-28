using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Repositories;

public class CharacterRepository : Repository<Character>, ICharacterRepository
{
    public CharacterRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<Character?> GetCharacterByNameAsync(string userName)
    {
        return await ApplySpecification(new CharacterByUserNameWithSnapshotsSpecification(userName))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false)
    {
        return await DbSet
            .Where(x => userNames.Contains(x.UserName) && x.NameChanged == includeNameChanged)
            .ToListAsync();
    }
}
