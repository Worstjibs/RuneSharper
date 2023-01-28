using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories;

public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
{
    public SnapshotRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<(Snapshot?, Snapshot?)> GetFirstAndLastSnapshots(string userName, DateRange dateRange)
    {
        var first = await ApplySpecification(
            new SnapshotBetweenDateRangeSpecification(userName, dateRange, Shared.Enums.FirstLast.First))
            .FirstOrDefaultAsync();

        if (first is null)
            return (null, null);

        var last = await ApplySpecification(
            new SnapshotBetweenDateRangeSpecification(userName, dateRange, Shared.Enums.FirstLast.Last))
            .FirstOrDefaultAsync();

        return (first, last);
    }

    public async Task<Snapshot?> GetLatestSnapshotAsync(string userName)
    {
        return await ApplySpecification(
            new LatestSnapshotByUserNameSpecification(userName))
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Snapshot>> GetLatestSnapshotsAsync(IEnumerable<string> userNames)
    {
        var result = await DbSet
            .Include(x => x.Skills)
            .Include(x => x.Activities)
            .Include(x => x.Character)
            .Where(x => userNames.Contains(x.Character.UserName))
            .GroupBy(x => new { x.Character.UserName })
            .Select(g => g.OrderByDescending(s => s.DateCreated).Take(1))            
            .ToListAsync();

        return result.SelectMany(x => x).ToList();
    }
}
