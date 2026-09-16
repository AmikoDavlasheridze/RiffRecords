using RiffRecords.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions
{
    public interface IWishlistedItemService
    {
        Task<List<WishlistedItemDto>> GetAllItemsAsync(int userId);
        Task<WishlistedItemDto> AddAnItemAsync (AddWishlistItemDto dto);
        Task RemoveItemAsync(int wishelistedItemId);
    }
}
