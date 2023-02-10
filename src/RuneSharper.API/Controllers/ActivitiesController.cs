using Microsoft.AspNetCore.Mvc;
using RuneSharper.Application.Models;
using RuneSharper.Application.Services.Snapshots;
using RuneSharper.Domain.Helpers;

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
