using RuneSharper.Domain.Entities.Snapshots;
using RuneSharper.Domain.Helpers;

namespace RuneSharper.Domain.Interfaces;

public interface ISkillSnapshotRepository
{
    Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange);
}