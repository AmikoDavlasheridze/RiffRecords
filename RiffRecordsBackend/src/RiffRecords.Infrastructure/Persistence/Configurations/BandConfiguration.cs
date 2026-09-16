using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class BandConfiguration : IEntityTypeConfiguration<Band>
    {
        public void Configure(EntityTypeBuilder<Band> builder)
        {
            builder.HasKey(b => b.BandId);

            builder.Property(b => b.BandName).IsRequired().HasMaxLength(256);
            builder.Property(b => b.Slug).IsRequired().HasMaxLength(256);
            builder.Property(b => b.Genre).IsRequired().HasMaxLength(100);
            builder.Property(B => B.BandPicture).IsRequired();

            builder.HasIndex(b => b.Slug).IsUnique();

            builder.HasMany(b => b.Vinyls)
                .WithOne(v => v.Band)
                .HasForeignKey(v => v.BandId);

            builder.HasMany(b => b.Achievements)
                .WithOne(a => a.Band)
                .HasForeignKey(a => a.BandId);

            builder.HasOne(b =>  b.Bio)
                .WithOne(bio => bio.Band)
                .HasForeignKey<Bio>(bio => bio.BandId);
        }
    }
}
