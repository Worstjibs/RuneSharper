
using RuneSharper.Domain.Entities;

namespace RuneSharper.Application.Services.SaveStats
{
    public interface ISaveStatsService
    {
        Task<Character> SaveStatsForCharacter(string username);
        Task SaveStatsForCharacters(IEnumerable<string> userNames);
    }
}