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

    public async Task<Character> SaveStatsForCharacter(string username)
    {
        var character = await _characterRepository.GetCharacterByNameAsync(username);

        character ??= new Character
        {
            UserName = username.ToLower(),
            Snapshots = new List<Snapshot>()
        };

        var snapshot = await _osrsApiService.QueryHiScoresByAccountAsync(character);

        ProcessCharacter(character, snapshot);

        await SaveCharacters();

        return character;
    }

    public async Task SaveStatsForCharacters(IEnumerable<string> usernames)
    {
        var accounts = (List<Character>)await _characterRepository.GetCharactersByNameAsync(usernames);

        var missingAccounts = usernames.Select(x => x.ToLower()).Except(accounts.Select(x => x.UserName.ToLower()));

        foreach (var username in missingAccounts)
        {
            var account = new Character { UserName = username.ToLower() };
            accounts.Add(account);
        }

        var results = await _osrsApiService.QueryHiScoresByAccountsAsync(accounts);

        foreach (var result in results)
        {
            ProcessCharacter(result.Key, result.Value);
        }

        await SaveCharacters();
    }

    private void ProcessCharacter(Character character, Snapshot? snapshot)
    {
        if (snapshot is null)
        {
            character.NameChanged = true;
            return;
        }

        character.Snapshots.Add(snapshot);
        snapshot.Character = character;

        if (character.Id == 0)
            _characterRepository.Insert(character);
    }

    private async Task SaveCharacters()
    {
        if (!await _characterRepository.Complete())
        {
            throw new DbUpdateException("No records updated");
        }
    }
}
