using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class BioConfiguration : IEntityTypeConfiguration<Bio>
    {
        public void Configure(EntityTypeBuilder<Bio> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.HistoryText).IsRequired().HasColumnType("nvarchar(max)");
            builder.Property(b => b.BioText).IsRequired().HasColumnType("nvarchar(max)");
        }
    }
}
