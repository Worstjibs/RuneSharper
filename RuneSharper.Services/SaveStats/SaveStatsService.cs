using Microsoft.EntityFrameworkCore;
using RuneSharper.Data.Repositories;
using RuneSharper.Services.Stats;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Services.SaveStats;

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
        var accounts = (List<Character>) await _characterRepository.GetCharactersByNameAsync(usernames);

        var missingAccounts = usernames.Select(x => x.ToLower()).Except(accounts.Select(x => x.UserName.ToLower()));

        foreach (var username in missingAccounts)
        {
            var account = new Character
            {
                UserName = username.ToLower(),
                Snapshots = new List<Snapshot>()
            };

            accounts.Add(account);
            _characterRepository.Insert(account);
        }
        
        var snapshots = await _osrsApiService.QueryHiScoresByAccountsAsync(accounts);

        accounts.Join(snapshots, x => x.UserName, x => x.Character.UserName, (x, y) =>
        {
            x.Snapshots.Add(y);
            return x;
        }).ToList();

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
        character.Snapshots.Add(await Task.Run(() => _osrsApiService.QueryHiScoresByAccountAsync(character)));

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
