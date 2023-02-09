using Microsoft.AspNetCore.Mvc;
using RuneSharper.Domain.Helpers;
using RuneSharper.Services.Models;
using RuneSharper.Services.Services.Snapshots;

namespace RuneSharper.API.Controllers;

public class ActivitiesController : BaseApiController
{
    private readonly ISnapshotsService _snapshotsService;

    public ActivitiesController(ISnapshotsService snapshotsService)
    {
        _snapshotsService = snapshotsService;
    }

    [HttpGet("{username}/activities-change")]
    public async Task<ActionResult<ActivitiesModel>> GetActivitiesChangeForUser(string userName, [FromQuery] DateRange dateRange)
    {
        var model = await _snapshotsService.GetActivitesChangeForUser(userName, dateRange);

        if (model is null)
            return NotFound("No Snapshots between date range");

        return Ok(model);
    }
}
