using RuneSharper.Domain.Entities;

namespace RuneSharper.Domain.Interfaces;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character?> GetCharacterByNameAsync(string userName);
    Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false);
}
