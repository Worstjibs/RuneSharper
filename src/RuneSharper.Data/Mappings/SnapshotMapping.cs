﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RuneSharper.Domain.Entities.Snapshots;

namespace RuneSharper.Data.Mappings; 
public class SnapshotMapping : IEntityTypeConfiguration<Snapshot> {
    public void Configure(EntityTypeBuilder<Snapshot> builder) {
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Character)
            .WithMany(x => x.Snapshots);
    }
}
