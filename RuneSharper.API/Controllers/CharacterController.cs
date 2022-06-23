using Microsoft.AspNetCore.Mvc;
using RuneSharper.Shared.Models;
using RuneSharper.Services.Characters;
using RuneSharper.Shared.Entities;

namespace RuneSharper.API.Controllers {
    public class CharacterController : BaseApiController {
        private readonly ICharactersService _charactersService;

        public CharacterController(ICharactersService charactersService) {
            _charactersService = charactersService;
        }

        /// <summary>
        /// Get the Character with specified Username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("{username}")]
        public async Task<ActionResult<Character>> Get(string username) {
            var character = await _charactersService.GetCharacterAsync(username);

            if (character == null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        /// <summary>
        /// Update stats for the specified Username,
        /// adding a new Snapshot node
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPut("{username}/update")]
        public async Task<ActionResult<Character>> UpdateCharacterStats(string username)
        {
            var character = await _charactersService.UpdateCharacterStats(username);

            if (character is null)
            {
                return NotFound();
            }

            return Ok(character);
        }

        /// <summary>
        /// Gets all Characters
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<CharacterListModel>>> GetList()
        {
            return Ok(await _charactersService.GetCharacterListModels());
        }
    }
}
