using Microsoft.Extensions.Caching.Memory;
using RuneSharper.Data.Extensions;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Repositories;

public class CachedCharacterRepository : Repository<Character>, ICharacterRepository
{
    private readonly CharacterRepository _characterRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedCharacterRepository(
        RuneSharperContext context,
        CharacterRepository characterRepository,
        IMemoryCache memoryCache) : base(context)
    {
        _characterRepository = characterRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Character?> GetCharacterByNameAsync(string userName)
    {
        return await _characterRepository.GetCharacterByNameAsync(userName);
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
}
