using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Repositories
{
    public class CharacterRepository : Repository<Character>, ICharacterRepository
    {
        public CharacterRepository(RuneSharperContext context) : base(context)
        {
        }

        public async Task<Character?> GetByCharacterNameAsync(string accountName)
        {
            return await DbSet
                .Include(x => x.Snapshots)
                .SingleOrDefaultAsync(x => x.UserName == accountName.ToLower());
        }
    }
}
