using RuneSharper.Shared.Entities.Snapshots;

namespace RuneSharper.Shared.Entities {
    public record Character : BaseIntEntity {
        public string UserName { get; set; } = default!;
        public ICollection<Snapshot> Snapshots { get; set; } = default!;
    }
}
