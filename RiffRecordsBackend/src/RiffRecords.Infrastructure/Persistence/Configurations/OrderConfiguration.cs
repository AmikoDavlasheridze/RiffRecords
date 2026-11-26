using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Order;
using RiffRecords.Domain.Entities.User;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(o => o.Id);

            // Stored as text so the table reads "InTransit", not 1.
            builder.Property(o => o.Status).HasConversion<string>().HasMaxLength(20);

            builder.Property(o => o.FullName).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Address).IsRequired().HasMaxLength(200);
            builder.Property(o => o.City).IsRequired().HasMaxLength(100);
            builder.Property(o => o.Phone).IsRequired().HasMaxLength(20);
            builder.Property(o => o.Total).HasPrecision(10, 2);

            // Deleting a user must not silently erase their order history.
            builder.HasOne<User>()
                .WithMany()
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(o => o.Items)
                .WithOne()
                .HasForeignKey(i => i.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Admin list filters by status.
            builder.HasIndex(o => o.Status);
        }
    }
}
