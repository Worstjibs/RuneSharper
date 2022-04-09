
using RuneSharper.Shared.Entities;

namespace RuneSharper.Services.SaveStats
{
    public interface ISaveStatsService
    {
        Task SaveStatsForCharacters(IEnumerable<string> userNames);
        Task<Character> SaveStatsForCharacter(string username);
    }
}