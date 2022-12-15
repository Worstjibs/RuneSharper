using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Models;

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

    public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = true)
    {
        return await ApplySpecification(new CharacterByUserNameWithSnapshotsSpecification(userNames))
            .ToListAsync();
    }
}
