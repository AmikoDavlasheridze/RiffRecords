using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RiffRecords.Domain.Entities.Band;


namespace RiffRecords.Infrastructure.Persistence.Configurations
{
    public class VinylConfiguration : IEntityTypeConfiguration<Vinyl>
    {
        public void Configure(EntityTypeBuilder<Vinyl> builder)
        {
            builder.HasKey(b => b.VinylId);

            builder.Property(v => v.Description).HasMaxLength(1000);
            builder.Property(v => v.Label).HasMaxLength(100);
            builder.Property(v => v.Price).HasPrecision(10, 2);
            builder.Property(b => b.AlbumTitle).IsRequired().HasMaxLength(200);
            builder.Property(b => b.Image).IsRequired();

            builder.HasMany(v => v.Tracks)
                .WithOne(t => t.Vinyl)
                .HasForeignKey(t => t.VinylId);

        }

    }
}
