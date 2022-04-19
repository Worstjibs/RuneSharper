using RuneSharper.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Entities.Snapshots
{
    public record ActivitySnapshot : SnapshotEntity<ActivityType>
    {
        public int Score { get; set; }
    }
}
