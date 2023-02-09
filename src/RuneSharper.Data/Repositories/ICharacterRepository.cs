using RuneSharper.Domain.Entities;

namespace RuneSharper.Data.Repositories;

public interface ICharacterRepository : IRepository<Character>
{
    Task<Character?> GetCharacterByNameAsync(string userName);
    Task<IEnumerable<Character>> GetCharactersByNameAsync(IEnumerable<string> userNames, bool includeNameChanged = false);
}
