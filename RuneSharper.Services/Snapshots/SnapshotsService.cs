using RuneSharper.Data.Repositories;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Snapshots;

public class SnapshotsService : ISnapshotsService
{
    private readonly ISnapshotRepository _snapshotRepository;

    public SnapshotsService(ISnapshotRepository snapshotRepository)
    {
        _snapshotRepository = snapshotRepository;
    }

    public async Task<StatsModel?> GetSnapshotChangeForUser(string userName, DateRange dateRange)
    {
        var (first, last) = await _snapshotRepository.GetFirstAndLastSnapshots(userName, dateRange);

        if (first == null)
            return null;

        var deltaList = first.Skills.Join(
            last!.Skills,
            f => f.Type,
            l => l.Type,
            (f, l) => new SkillSnapshot
            {
                Experience = l.Experience - f.Experience,
                Level = l.Level - f.Level,
                Rank = f.Rank - l.Rank,
                Type = l.Type
            }).ToList();

        return new StatsModel(deltaList);
    }
}
