using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Entities.NgxCharts;

public class LineChartModels
{
    public string Name { get; set; } = default!;
    public IEnumerable<LineChartSeriesData> Series { get; set; } = default!;
}
