using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Enums;
using RuneSharper.Shared.Models;

namespace RuneSharper.Services.Characters;

public interface ICharactersService
{
    Task<Character?> GetCharacterAsync(string username);
    Task<Character?> UpdateCharacterStats(string username);
    Task<IEnumerable<CharacterListModel>> GetCharacterListModels(string? sort, SortDirection direction);
}