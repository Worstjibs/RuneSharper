using DotnetOsrsApiWrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuneSharper.Services.Stats;
using RuneSharper.Shared;

namespace RuneSharper.API.Controllers {
    public class StatsController : BaseApiController {
        private readonly IStatsService _statsService;

        public StatsController(IStatsService statsService) {
            _statsService = statsService;
        }

        /// <summary>
        /// Get Stats for a specified Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public ActionResult<IEnumerable<Skill>> GetStats(string username) {
            return Ok(_statsService.QueryStatsForUserId(username));
        }
    }
}
