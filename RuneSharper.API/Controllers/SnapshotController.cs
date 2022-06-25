using Microsoft.AspNetCore.Mvc;
using RuneSharper.Shared.Models;
using RuneSharper.Data.Repositories;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.API.Controllers
{
    public class SnapshotController : BaseApiController
    {
        private readonly ISkillSnapshotRepository _skillSnapshotRepository;

        public SnapshotController(ISkillSnapshotRepository skillSnapshotRepository)
        {
            _skillSnapshotRepository = skillSnapshotRepository;
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
    }
}
