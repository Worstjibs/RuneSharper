using Microsoft.Extensions.Caching.Memory;
using RuneSharper.Application.Models;
using RuneSharper.Data.Extensions;
using RuneSharper.Domain.Interfaces;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Data.Repositories.Characters;

public class CachedProjectedCharacterRepository : CachedCharacterRepository, IProjectedCharacterRepository<CharacterListModel>
{
    private readonly ProjectedCharacterRepository _projectedCharacterRepository;

    public CachedProjectedCharacterRepository(
        RuneSharperContext context,
        CharacterRepository characterRepository,
        ProjectedCharacterRepository projectedCharacterRepository,
        IMemoryCache memoryCache)
        : base(
            context,
            characterRepository,
            memoryCache)
    {
        _projectedCharacterRepository = projectedCharacterRepository;
    }

    public async Task<IEnumerable<CharacterListModel>> GetProjectedCharacters(string? orderBy, SortDirection? direction)
    {
        if (orderBy is null || direction is null)
            return await _projectedCharacterRepository.GetProjectedCharacters(orderBy, direction);

        var key = $"{orderBy}-{direction}";

        var result = await _memoryCache.GetOrCreateAsync(key, entry =>
        {
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(30);

            return _projectedCharacterRepository.GetProjectedCharacters(orderBy, direction);
        });

        if (result is null)
            return Enumerable.Empty<CharacterListModel>();

        return result;
    }
}
