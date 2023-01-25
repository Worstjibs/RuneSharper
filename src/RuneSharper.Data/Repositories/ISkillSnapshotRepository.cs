using RuneSharper.Shared.Entities.Snapshots;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories
{
    public interface ISkillSnapshotRepository
    {
        Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange);
    }
}