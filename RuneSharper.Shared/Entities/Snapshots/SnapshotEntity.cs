using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RuneSharper.Shared.Entities.Snapshots
{
    public record SnapshotEntity<T> : BaseIntEntity where T : Enum
    {
        public T Type { get; set; } = default!;
        public int Rank { get; set; }
        public Snapshot Snapshot { get; set; } = default!;
    }
}
