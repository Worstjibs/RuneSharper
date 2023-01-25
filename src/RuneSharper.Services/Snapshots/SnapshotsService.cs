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

    public async Task<StatsModel?> GetStatsChangeForUser(string userName, DateRange dateRange)
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

    public async Task<ActivitiesModel?> GetActivitesChangeForUser(string userName, DateRange dateRange)
    {
        var (first, last) = await _snapshotRepository.GetFirstAndLastSnapshots(userName, dateRange);

        if (first is null)
            return null;

        var deltaList = first.Activities.Join(
            last!.Activities,
            f => f.Type,
            l => l.Type,
            (f, l) => new ActivitySnapshot
            {
                Rank = f.Rank - l.Rank,
                Score = l.Score - f.Score,
                Type = f.Type
            }).ToList();

        return new ActivitiesModel(deltaList);
    }
}
