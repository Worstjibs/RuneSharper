using RuneSharper.Shared.Entities;

namespace RuneSharper.Services.Characters
{
    public interface ICharactersService
    {
        Task<Character?> GetCharacterAsync(string username);
        Task<Character?> UpdateCharacterStats(string username);
    }
}