using RiffRecords.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions.serviceAbstractions
{
    public interface IWishlistedItemService
    {
        Task<List<WishlistedItemDto>> GetAllItemsAsync(int userId);
        Task<WishlistedItemDto> AddAnItemAsync (int userId, AddWishlistItemDto dto);
        Task RemoveItemAsync(int userId, int wishelistedItemId);
    }
}
