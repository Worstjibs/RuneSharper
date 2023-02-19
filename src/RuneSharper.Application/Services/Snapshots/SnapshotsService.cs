using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Helpers;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Application.Models;

namespace RuneSharper.Application.Services.Snapshots;

public class SnapshotsService : ISnapshotsService
{
    private readonly ISnapshotRepository _snapshotRepository;

    public SnapshotsService(ISnapshotRepository snapshotRepository)
    {
        _snapshotRepository = snapshotRepository;
    }

    public async Task<StatsModel?> GetStatsChangeForUser(string userName, DateRange dateRange)
    {
        var first = await _snapshotRepository.GetFirstSnapshotAsync(userName, dateRange);

        if (first is null)
            return null;

        var last = await _snapshotRepository.GetLastSnapshotAsync(userName, dateRange);

        var deltaList = first.Skills.Join(
            last!.Skills,
            f => f.Type,
            l => l.Type,
            (f, l) =>
                f.Rank < 0
                ? null
                : new SkillSnapshot
                {
                    Experience = l.Experience - f.Experience,
                    Level = l.Level - f.Level,
                    Rank = f.Rank - l.Rank,
                    Type = l.Type
                })
            .Where(x => x is not null)
            .ToList();

        return new StatsModel(deltaList);
    }

    public async Task<ActivitiesModel?> GetActivitesChangeForUser(string userName, DateRange dateRange)
    {
        var first = await _snapshotRepository.GetFirstSnapshotAsync(userName, dateRange);

        if (first is null)
            return null;

        var last = await _snapshotRepository.GetLastSnapshotAsync(userName, dateRange);

        var deltaList = first.Activities.Join(
            last!.Activities,
            f => f.Type,
            l => l.Type,
            (f, l) =>
                f.Rank < 0
                ? null
                : new ActivitySnapshot
                {
                    Rank = f.Rank - l.Rank,
                    Score = l.Score - f.Score,
                    Type = f.Type
                })
            .Where(x => x is not null)
            .ToList();

        return new ActivitiesModel(deltaList!);
    }
}
