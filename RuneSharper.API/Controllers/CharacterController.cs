using DotnetOsrsApiWrapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.Stats;
using RuneSharper.Shared;
using RuneSharper.Shared.Entities;

namespace RuneSharper.API.Controllers {
    public class CharacterController : BaseApiController {
        private readonly CharacterRepository _characterRepository;

        public CharacterController(CharacterRepository characterRepository) {
            _characterRepository = characterRepository;
        }

        /// <summary>
        /// Get the Character with specified Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<Character>> Get(string username) {
            var character = await _characterRepository.GetCharacterByNameAsync(username);

            return Ok(character);
        }
    }
}
