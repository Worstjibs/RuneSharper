using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Data.Repositories;

public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
{
    public SnapshotRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<Snapshot?> GetLatestSnapshotAsync(string userName)
    {
        return await ApplySpecification(
            new LatestSnapshotByUserNameSpecification(userName))
            .FirstOrDefaultAsync();
    }

    public async Task<Dictionary<string, Snapshot>> GetLatestSnapshotsAsync(IEnumerable<string> userNames)
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
