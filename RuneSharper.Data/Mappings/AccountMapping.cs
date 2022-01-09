using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Mappings {
    public class AccountMapping : IEntityTypeConfiguration<Account> {
        public void Configure(EntityTypeBuilder<Account> builder) {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.Snapshots)
                .WithOne(x => x.Account);
        }
    }
}
