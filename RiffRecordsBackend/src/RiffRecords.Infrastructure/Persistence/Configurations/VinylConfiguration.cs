using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class VinylConfiguration : IEntityTypeConfiguration<Vinyl>
    {
        public void Configure(EntityTypeBuilder<Vinyl> builder)
        {
            builder.HasKey(b => b.VinylId);

            builder.Property(b => b.AlbumTitle).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Price).IsRequired();
            builder.Property(b => b.Image).IsRequired();

            builder.HasMany(v => v.Tracks)
                .WithOne(t => t.Vinyl)
                .HasForeignKey(t => t.VinylId);

        }

    }
}
