using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions
{
    public interface IWishlistedItemRepository
    {
        Task<WishlistedItem?> GetWishlistByIdAsync(int id);
        Task<List<WishlistedItem>> GetWishlistedItemsAsync(int userId);
        Task<WishlistedItem?> GetByUserAndVinylAsync(int userId, int vinylId);
        Task AddAsync(WishlistedItem item);
        void Remove(WishlistedItem item);
        
    }
}
