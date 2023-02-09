using Microsoft.AspNetCore.Mvc;
using RuneSharper.Shared.Models;
using RuneSharper.Domain.Helpers;
using RuneSharper.Services.Snapshots;
using RuneSharper.Services.Characters;
using RuneSharper.Domain.Interfaces;

namespace RuneSharper.API.Controllers;

public class SnapshotController : BaseApiController
{
    private readonly ISkillSnapshotRepository _skillSnapshotRepository;
    private readonly ISnapshotsService _snapshotsService;
    private readonly ICharactersService _charactersService;

    public SnapshotController(ISkillSnapshotRepository skillSnapshotRepository, ISnapshotsService snapshotsService, ICharactersService charactersService)
    {
        _skillSnapshotRepository = skillSnapshotRepository;
        _snapshotsService = snapshotsService;
        _charactersService = charactersService;
    }

    [HttpGet("{username}")]
    public async Task<ActionResult<IEnumerable<SkillSnapshotModel>>> GetSkillSnapshotsForUser(string username, [FromQuery] DateRange dateRange)
    {
        var result = await _skillSnapshotRepository.GetByUsername(username, dateRange);

        var models = result.Select(x => new SkillSnapshotModel
        {
            Type = x.Type.ToString(),
            DateCreated = x.Snapshot.DateCreated,
            Experience = x.Experience,
            Level = x.Level,
            Rank = x.Rank
        });

        return Ok(models);
    }

    [HttpGet("{username}/stats-change")]
    public async Task<ActionResult<StatsModel>> GetStatsChangeForUser(string username, [FromQuery] DateRange dateRange)
    {
        var user = await _charactersService.GetCharacterAsync(username);

        if (user is null)
            return NotFound($"Character with name {username} not found in RuneSharper.");

        var model = await _snapshotsService.GetStatsChangeForUser(username, dateRange);

        if (model is null)
            return NotFound("No Snapshots between date range");

        return model;
    }
}
