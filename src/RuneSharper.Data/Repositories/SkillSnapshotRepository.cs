using Microsoft.EntityFrameworkCore;
using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Helpers;
using RuneSharper.Domain.Interfaces;

namespace RuneSharper.Data.Repositories;

public class SkillSnapshotRepository : Repository<SkillSnapshot>, ISkillSnapshotRepository
{
    public SkillSnapshotRepository(RuneSharperContext context, IRuneSharperConnectionFactory connectionFactory) 
        : base(context, connectionFactory)
    {
    }

    public async Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange)
    {
        return await DbSet
            .Include(x => x.Snapshot)
            .Where(x => x.Snapshot.DateCreated.Date >= dateRange.DateFrom
                && x.Snapshot.DateCreated.Date < dateRange.DateTo
                && x.Snapshot.Character.UserName == username)
            .ToListAsync();
    }
}
