using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Services.SaveStats
{
    public class SaveStatsService : ISaveStatsService
    {
        private readonly IOsrsApiService _osrsApiService;
        private readonly ICharacterRepository _characterRepository;

        public SaveStatsService(IOsrsApiService osrsApiService, ICharacterRepository characterRepository)
        {
            _osrsApiService = osrsApiService;
            _characterRepository = characterRepository;
        }

        public async Task SaveStatsForCharacters(IEnumerable<string> usernames)
        {
            foreach (var username in usernames)
            {
                var account = await _characterRepository.GetCharacterByNameAsync(username);

                if (account is null)
                {
                    account = new Character
                    {
                        UserName = username.ToLower(),
                        Snapshots = new List<Snapshot>()
                    };

                    _characterRepository.Insert(account);
                }

                account.Snapshots.Add(_osrsApiService.QueryHiScoresByAccount(account));
            }

            await SaveCharacters();
        }

        public async Task<Character> SaveStatsForCharacter(string username)
        {
            var character = await CreateSnapshotForCharacter(username);

            await SaveCharacters();

            return character;
        }

        private async Task<Character> CreateSnapshotForCharacter(string username)
        {
            var character = await _characterRepository.GetCharacterByNameAsync(username);

            if (character is null)
            {
                character = new Character
                {
                    UserName = username.ToLower(),
                    Snapshots = new List<Snapshot>()
                };

                _characterRepository.Insert(character);
            }
            character.Snapshots.Add(await Task.Run(() => _osrsApiService.QueryHiScoresByAccount(character)));

            return character;
        }

        private async Task SaveCharacters()
        {
            if (!await _characterRepository.Complete())
            {
                throw new DbUpdateException("No records updated");
            }
        }
    }
}
