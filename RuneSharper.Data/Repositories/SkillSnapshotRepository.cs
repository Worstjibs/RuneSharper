using Microsoft.EntityFrameworkCore;
using RuneSharper.Shared.Entities;
using RuneSharper.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Data.Repositories
{
    public class SkillSnapshotRepository : Repository<SkillSnapshot>, ISkillSnapshotRepository
    {
        public SkillSnapshotRepository(RuneSharperContext context) : base(context)
        {
        }

        public async Task<IEnumerable<SkillSnapshot>> GetByUsername(string username, DateRange dateRange)
        {
            return await DbSet
                .Where(x => x.DateCreated >= dateRange.DateFrom
                    && x.DateCreated <= dateRange.DateTo
                    && x.Snapshot.Character.UserName == username)
                .ToListAsync();
        }
    }
}
