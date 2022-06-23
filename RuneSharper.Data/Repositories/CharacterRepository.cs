using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Models;

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

        public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> accountNames)
        {
            return await DbSet
                .Include(x => x.Snapshots)
                .Where(x => accountNames.Contains(x.UserName))
                .ToListAsync();
        }

        //public async Task<IEnumerable<Character>> GetCharacterListModelsAsync()
        //{
        //    return await DbSet
        //        .Include(x => x.Snapshots.OrderByDescending(x => x.DateCreated)).ThenInclude(x => x.Skills)
        //        .Include(x => x.Snapshots.OrderByDescending(x => x.DateCreated)).ThenInclude(x => x.Activities)
        //        .Select(x => new CharacterListModel
        //        {
        //            UserName = x.UserName,
        //            LatestSnapshot = x.LatestSnapshot
        //        })
        //        .ToListAsync();

        //}
    }
}
