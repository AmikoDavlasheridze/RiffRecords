using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class TrackConfiguration : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Property(t => t.TrackName).IsRequired().HasMaxLength(100);
        }
    }
}
