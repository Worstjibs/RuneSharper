using Microsoft.EntityFrameworkCore;
using RuneSharper.Data;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;

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

        public async Task SaveStatsForUsers(IEnumerable<string> userNames)
        {
            foreach (var accountName in userNames)
            {
                var account = await _characterRepository.GetCharacterByNameAsync(accountName);

                if (account is null)
                {
                    account = new Character
                    {
                        UserName = accountName.ToLower(),
                        Snapshots = new List<Snapshot>()
                    };

                    _characterRepository.Insert(account);
                }

                account.Snapshots.Add(_osrsApiService.QueryHiScoresByAccount(account));

                if (!await _characterRepository.Complete())
                {
                    throw new DbUpdateException("No records updated");
                }
            }
        }
    }
}
