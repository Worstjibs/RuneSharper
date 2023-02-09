using RuneSharper.Domain.Entities.NgxCharts;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.LineCharts
{
    public interface ILineChartService
    {
        Task<IEnumerable<LineChartModels>> GetSkillSnapshotData(string username, DateRange dateRange, bool includeOverall);
    }
}