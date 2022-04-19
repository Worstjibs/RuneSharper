using Microsoft.AspNetCore.Mvc;
using RuneSharper.Data.Repositories;
using RuneSharper.Shared.Entities;
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
        public async Task<ActionResult<IEnumerable<SkillSnapshot>>> GetSkillSnapshotsForUser(string username, [FromQuery] DateRange dateRange)
        {
            var result = await _skillSnapshotRepository.GetByUsername(username, dateRange);

            return Ok(result);
        }
    }
}
