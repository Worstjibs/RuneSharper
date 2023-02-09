using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Specifications;
using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories;

public class SnapshotRepository : Repository<Snapshot>, ISnapshotRepository
{
    public SnapshotRepository(RuneSharperContext context) : base(context)
    {
    }

    public async Task<Snapshot?> GetFirstSnapshotAsync(string userName, DateRange dateRange)
    {
        return await ApplySpecification(
            new SnapshotBetweenDateRangeSpecification(userName, dateRange, FirstLast.First))
            .FirstOrDefaultAsync();
    }

    public async Task<Snapshot?> GetLastSnapshotAsync(string userName, DateRange dateRange)
    {
        return await ApplySpecification(
            new SnapshotBetweenDateRangeSpecification(userName, dateRange, FirstLast.Last))
            .FirstOrDefaultAsync();
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
