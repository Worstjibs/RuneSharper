using RuneSharper.Data.Repositories;
using RuneSharper.Services.SaveStats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;
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
        private readonly ISnapshotRepository _snapshotRepository;
        private readonly ISaveStatsService _saveStatsService;

        public CharactersService(
            ICharacterRepository characterRepository,
            ISnapshotRepository snapshotRepository,
            ISaveStatsService saveStatsService)
        {
            _characterRepository = characterRepository;
            _snapshotRepository = snapshotRepository;
            _saveStatsService = saveStatsService;
        }

        public async Task<Character?> GetCharacterAsync(string username)
        {
            return await _characterRepository.GetCharacterByNameAsync(username);
        }

        public async Task<Character?> UpdateCharacterStats(string username)
        {
            // NOTE: This should create message on Azure Service Bus
            // I need to write a RuneSharper Message Service to wrap this properly
            return await _saveStatsService.SaveStatsForCharacter(username);
        }

        public async Task<IEnumerable<CharacterListModel>> GetCharacterListModels()
        {
            var characters = await _characterRepository.GetAllAsync();
            var latestSnapshots = await _snapshotRepository.GetLatestSnapshots(characters.Select(x => x.UserName));

            var characterModels = characters.Select(x => new CharacterListModel
            {
                UserName = x.UserName,
                Stats = new StatsModel(latestSnapshots[x.UserName])
            }).ToList();

            return characterModels;
        }
    }
}
