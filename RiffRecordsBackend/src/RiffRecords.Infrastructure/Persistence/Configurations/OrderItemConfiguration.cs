using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Band;
using RiffRecords.Domain.Entities.Order;

namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.AlbumTitle).IsRequired().HasMaxLength(200);
            builder.Property(i => i.UnitPrice).HasPrecision(10, 2);
            builder.Ignore(i => i.LineTotal);

            // A vinyl that has been ordered can't be deleted, so order history stays intact.
            builder.HasOne<Vinyl>()
                .WithMany()
                .HasForeignKey(i => i.VinylId)
                .OnDelete(DeleteBehavior.Restrict);

            // Order.AddItem merges duplicates; the database enforces it too.
            builder.HasIndex(i => new { i.OrderId, i.VinylId }).IsUnique();
        }
    }
}
