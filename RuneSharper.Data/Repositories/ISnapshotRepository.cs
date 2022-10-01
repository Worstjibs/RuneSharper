using RuneSharper.Shared.Entities.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Data.Repositories
{
    public interface ISnapshotRepository : IRepository<Snapshot>
    {
        public Task<Dictionary<string, Snapshot>> GetLatestSnapshotByCharacter(IEnumerable<string> userNames);
    }
}
