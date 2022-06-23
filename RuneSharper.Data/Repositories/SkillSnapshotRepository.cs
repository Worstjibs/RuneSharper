using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories
{
    public class SkillSnapshotRepository : Repository<SkillSnapshot>, ISkillSnapshotRepository
    {
        public SkillSnapshotRepository(RuneSharperContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange)
        {
            return await DbSet
                .Include(x => x.Snapshot)
                .Where(x => x.DateCreated.Date >= dateRange.DateFrom
                    && x.DateCreated.Date <= dateRange.DateTo
                    && x.Snapshot.Character.UserName == username)
                .ToListAsync();
        }
    }
}
