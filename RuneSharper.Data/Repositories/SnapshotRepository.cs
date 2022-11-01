using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Data.Repositories;

public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
{
    public SnapshotRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<Snapshot?> GetLatestSnapshotAsync(string userName)
    {
        return await DbSet
            .Include(x => x.Skills)
            .Include(x => x.Activities)
            .OrderByDescending(x => x.DateCreated)
            .Where(x => x.Character.UserName == userName)
            .Take(1)
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary<string, Snapshot>> GetLatestSnapshotByCharacter(IEnumerable<string> userNames)
    {
        var grouping = await DbSet
            .Include(x => x.Skills)
            .Where(x => userNames.Contains(x.Character.UserName))
            .GroupBy(x => x.Character.UserName)
            .Select(g => g.OrderByDescending(s => s.DateCreated).Take(1))
            .ToListAsync();

        return grouping.SelectMany(x => x).ToDictionary(x => x.Character.UserName, x => x);
    }
}
