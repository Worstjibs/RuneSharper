using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Mappings {
    public class SkillSnapshotMapping : IEntityTypeConfiguration<Snapshot> {
        public void Configure(EntityTypeBuilder<Snapshot> builder) {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Account)
                .WithMany(x => x.Snapshots);
        }
    }
}
