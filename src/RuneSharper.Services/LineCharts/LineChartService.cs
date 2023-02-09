using RuneSharper.Domain.Entities.NgxCharts;
using RuneSharper.Domain.Enums;
using RuneSharper.Domain.Helpers;
using RuneSharper.Domain.Interfaces;

namespace RuneSharper.Services.LineCharts;

public class LineChartService : ILineChartService
{
    private readonly ISkillSnapshotRepository _skillSnapshotRepository;

    public LineChartService(ISkillSnapshotRepository skillSnapshotRepository)
    {
        _skillSnapshotRepository = skillSnapshotRepository;
    }

    public async Task<IEnumerable<LineChartModels>> GetSkillSnapshotData(string username, DateRange dateRange, bool includeOverall)
    {
        var skillSnapshotData = await _skillSnapshotRepository.GetByUsername(username, dateRange);

        if (!includeOverall)
        {
            skillSnapshotData = skillSnapshotData.Where(x => x.Type != SkillType.Overall);
        }

        var lineChartData = skillSnapshotData
            .GroupBy(x => new { x.Type })
            .Select(x => new LineChartModels
            {
                Name = x.Key.Type.ToString(),
                Series = x.GroupBy(g => g.Snapshot.DateCreated.ToString("yyyy-MM-dd HH:ss"))
                    .Select(g => new LineChartSeriesData { 
                        Name = g.Key,
                        Value = g.Max(s => s.Experience).ToString()
                    }).ToList()
            }).ToList();

        return lineChartData;
    }
}
