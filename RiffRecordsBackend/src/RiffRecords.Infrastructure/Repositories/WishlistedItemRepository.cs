using Microsoft.EntityFrameworkCore;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities.User;
using RiffRecords.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Repositories
{
    public class WishlistedItemRepository : IWishlistedItemRepository
    {
        private readonly AppDbContext _context;

        public WishlistedItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(WishlistedItem item)
        {
            await _context.WishlistedItems.AddAsync(item);
        }

        public async Task<WishlistedItem?> GetByUserAndVinylAsync(int userId, int vinylId)
        {
            return await _context.WishlistedItems
                .FirstOrDefaultAsync(a => a.UserId == userId && a.VinylId == vinylId);
                
        }

        public async Task<List<WishlistedItem>> GetByUserIdAsync(int userId)
        {
            return await _context.WishlistedItems
                .Where(w => w.UserId == userId)
                .ToListAsync();
        }

        public async Task<WishlistedItem?> GetWishlistByIdAsync(int id)
        {
            return await _context.WishlistedItems.FirstOrDefaultAsync(w => w.Id == id);
        }

        public void Remove(WishlistedItem item)
        {
            _context.WishlistedItems.Remove(item);
        }
    }
}
