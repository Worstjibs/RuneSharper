using RuneSharper.Domain.Entities;
using RuneSharper.Shared.Enums;
using RuneSharper.Services.Models;

namespace RuneSharper.Services.Services.Characters;

public interface ICharactersService
{
    Task<Character?> GetCharacterAsync(string userName);
    Task<Character?> UpdateCharacterStatsAsync(string userName);
    Task<IEnumerable<CharacterListModel>> GetCharacterListModelsAsync(string? sort, SortDirection direction);
    Task<CharacterViewModel?> GetCharacterViewModelAsync(string userName);
}