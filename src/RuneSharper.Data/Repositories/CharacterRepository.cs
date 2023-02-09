using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Interfaces;

namespace RuneSharper.Data.Repositories;

public class CharacterRepository : Repository<Character>, ICharacterRepository
{
    public CharacterRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<Character?> GetCharacterByNameAsync(string userName)
    {
        return await ApplySpecification(new CharacterByUserNameSpecification(userName))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false)
    {
        return await ApplySpecification(new CharacterByUserNameSpecification(userNames))
            .ToListAsync();
    }
}
