using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class WishlistedItemConfiguration : IEntityTypeConfiguration<WishlistedItem>
    {
        public void Configure(EntityTypeBuilder<WishlistedItem> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasIndex(a => new { a.UserId, a.VinylId }).IsUnique();

        }
    }
}
