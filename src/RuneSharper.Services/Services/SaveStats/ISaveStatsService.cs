
using RuneSharper.Domain.Entities;

namespace RuneSharper.Services.Services.SaveStats
{
    public interface ISaveStatsService
    {
        Task<Character> SaveStatsForCharacter(string username);
        Task SaveStatsForCharacters(IEnumerable<string> userNames);
    }
}