using RuneSharper.Data.Repositories;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Services.Characters
{
    public class CharactersService : ICharactersService
    {
        private readonly ICharacterRepository _characterRepository;
        private readonly ISaveStatsService _saveStatsService;

        public CharactersService(ICharacterRepository characterRepository, ISaveStatsService saveStatsService)
        {
            _characterRepository = characterRepository;
            _saveStatsService = saveStatsService;
        }

        public async Task<Character?> GetCharacterAsync(string username)
        {
            return await _characterRepository.GetCharacterByNameAsync(username);
        }

        public async Task<Character?> UpdateCharacterStats(string username)
        {
            return await _saveStatsService.SaveStatsForCharacter(username);
        }
    }
}
