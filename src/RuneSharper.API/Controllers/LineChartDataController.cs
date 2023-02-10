using Microsoft.AspNetCore.Mvc;
using RuneSharper.Application.Services.LineCharts;
using RuneSharper.Domain.Entities.NgxCharts;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.API.Controllers;

public class LineChartDataController : BaseApiController
{
    private readonly ILineChartService _lineChartService;

    public LineChartDataController(ILineChartService lineChartService)
    {
        _lineChartService = lineChartService;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<LineChartModels>>> GetSkillSnapshotData(string username, [FromQuery] DateRange dateRange, bool includeOverall = false)
    {
        var data = await _lineChartService.GetSkillSnapshotData(username, dateRange, includeOverall);

        return Ok(data);
    }
}
