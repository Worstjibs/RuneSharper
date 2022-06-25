using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuneSharper.Shared.Entities;

namespace RuneSharper.Data.Mappings;

public class CharacterMapping : IEntityTypeConfiguration<Character> {
    public void Configure(EntityTypeBuilder<Character> builder) {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Snapshots)
            .WithOne(x => x.Character);
    }
}
