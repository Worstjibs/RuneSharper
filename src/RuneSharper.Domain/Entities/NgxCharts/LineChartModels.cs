namespace RuneSharper.Domain.Entities.NgxCharts;

public class LineChartModels
{
    public string Name { get; set; } = default!;
    public IEnumerable<LineChartSeriesData> Series { get; set; } = default!;
}
