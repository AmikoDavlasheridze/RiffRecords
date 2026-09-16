using Microsoft.EntityFrameworkCore;
using RiffRecords.Domain.Entities.Band;
using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Band> Bands => Set<Band>();
        public DbSet<Vinyl> Vinyls => Set<Vinyl>();
        public DbSet<User> Users => Set<User>();
        public DbSet<WishlistedItem> WishlistedItems => Set<WishlistedItem>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }
    }
}
