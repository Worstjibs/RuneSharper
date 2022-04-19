using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Helpers;

namespace RuneSharper.Data.Repositories
{
    public interface ISkillSnapshotRepository
    {
        Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange);
    }
}