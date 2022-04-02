
namespace RuneSharper.Services.SaveStats
{
    public interface ISaveStatsService
    {
        Task SaveStatsForUsers(IEnumerable<string> userNames);
    }
}