using RuneSharper.Data.Repositories;
using RuneSharper.Shared.Entities.NgxCharts;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.LineCharts
{
    public class LineChartService : ILineChartService
    {
        private readonly ISkillSnapshotRepository _skillSnapshotRepository;

        public LineChartService(ISkillSnapshotRepository skillSnapshotRepository)
        {
            _skillSnapshotRepository = skillSnapshotRepository;
        }

        public async Task<IEnumerable<LineChartData>> GetSkillSnapshotData(string username, DateRange dateRange, bool includeOverall)
        {
            var skillSnapshotData = await _skillSnapshotRepository.GetByUsername(username, dateRange);

            if (!includeOverall)
            {
                skillSnapshotData = skillSnapshotData.Where(x => x.Type != SkillType.Overall);
            }

            var lineChartData = skillSnapshotData
                .GroupBy(x => new { x.Type })
                .Select(x => new LineChartData
                {
                    Name = x.Key.Type.ToString(),
                    Series = x.GroupBy(g => g.DateCreated.ToString("yyyy-MM-dd"))
                        .Select(g => new LineChartSeriesData { 
                            Name = g.Key,
                            Value = g.Max(s => s.Experience).ToString()
                        }).ToList()
                }).ToList();

            return lineChartData;
        }
    }
}
