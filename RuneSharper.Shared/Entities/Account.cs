﻿namespace RuneSharper.Shared.Entities {
    public record Account : BaseIntEntity {
        public string UserName { get; set; } = default!;
        public ICollection<Snapshot> Snapshots { get; set; } = default!;
    }
}
