using RuneSharper.Domain.Entities;
using RuneSharper.Shared.Enums;

namespace RuneSharper.Domain.Interfaces;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character?> GetCharacterByNameAsync(string userName);
    Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false);
    Task<IEnumerable<Character>> GetCharactersAsync(string? sortTable, string? sortColumn, SortDirection direction, int skip, int take);
}
