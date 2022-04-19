using RuneSharper.Shared.Entities.NgxCharts;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Services.LineCharts
{
    public interface ILineChartService
    {
        Task<IEnumerable<LineChartData>> GetSkillSnapshotData(string username, DateRange dateRange, bool includeOverall);
    }
}