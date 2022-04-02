using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Repositories
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        public CharacterRepository(RuneSharperContext context) : base(context)
        {
        }

        public async Task<Character?> GetCharacterByNameAsync(string accountName)
        {
            return await DbSet
                .Include(x => x.Snapshots)
                .SingleOrDefaultAsync(x => x.UserName == accountName.ToLower());
        }

        public async Task<IEnumerable<Character?>> GetCharactersByNameAsync(IEnumerable<string> accountNames)
        {
            return await DbSet
                .Include(x => x.Snapshots)
                .Where(x => accountNames.Contains(x.UserName))
                .ToListAsync();
        }
    }
}
