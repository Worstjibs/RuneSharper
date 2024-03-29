﻿using Microsoft.Extensions.Caching.Memory;
using RuneSharper.Data.Extensions;
using RuneSharper.Domain.Entities;
using RuneSharper.Domain.Interfaces;

namespace RuneSharper.Data.Repositories.Characters;

public class CachedCharacterRepository : Repository<Character>, ICharacterRepository
{
    protected readonly CharacterRepository _characterRepository;
    protected readonly IMemoryCache _memoryCache;

    public CachedCharacterRepository(
        RuneSharperContext context,
        CharacterRepository characterRepository,
        IMemoryCache memoryCache)
        : base(context)
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
}
