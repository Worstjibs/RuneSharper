using Microsoft.Extensions.Caching.Memory;
using RuneSharper.Data.Extensions;
using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Data.Repositories;

public class CachedCharacterRepository : Repository<Character>, ICharacterRepository
{
    private readonly CharacterRepository _characterRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedCharacterRepository(
        RuneSharperContext context,
        CharacterRepository characterRepository,
        IRuneSharperConnectionFactory connectionFactory,
        IMemoryCache memoryCache) : base(context, connectionFactory)
    {
        _characterRepository = characterRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Character?> GetCharacterByNameAsync(string userName)
    {
        var character = await _memoryCache.GetOrCreateAsync($"character-{userName}", async entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

            return await _characterRepository.GetCharacterByNameAsync(userName);
        });

        if (character is not null)
            _context.Characters.AttachRange(character);

        return character;
    }

    public async Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false)
    {
        var results = await _memoryCache.GetOrCreateAsync(
            userNames,
            x => $"character-{x.ToLower()}",
            x => _characterRepository.GetCharactersByNameAsync(x, includeNameChanged),
            x => x.UserName,
            TimeSpan.FromSeconds(30));

        _context.Characters.AttachRange(results);

        return results;
    }

    public async Task<IEnumerable<Character>> GetCharactersAsync(string? sortTable, string? sortColumn, SortDirection direction, int skip, int take)
    {
        return await _characterRepository.GetCharactersAsync(sortTable, sortColumn, direction, skip, take);
    }
}
