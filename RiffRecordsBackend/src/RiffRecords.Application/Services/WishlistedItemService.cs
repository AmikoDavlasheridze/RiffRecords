using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Dtos;
using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Services
{
    public class WishlistedItemService : IWishlistedItemService
    {
        private readonly IWishlistedItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public WishlistedItemService(IWishlistedItemRepository repository, IUnitOfWork unitOfWork)
        {
            repository = _repository;
            unitOfWork = _unitOfWork;
        }

        public async Task<WishlistedItemDto> AddAnItemAsync(AddWishlistItemDto dto)
        {
            var existing = await _repository.GetByUserAndVinylAsync(dto.UserId, dto.VinylId);
            if (existing != null)
                throw new InvalidOperationException("This Vinyl Is Already In The Wishlist");


            var item = new WishlistedItem
            {
                UserId = dto.UserId,
                VinylId = dto.VinylId,
            };

            await _repository.AddAsync(item);
            await _unitOfWork.SaveChangesAsync();

            return new WishlistedItemDto(item.Id, item.UserId, item.VinylId);
        }

        public async Task<List<WishlistedItemDto>> GetAllItemsAsync(int userId)
        {
            var items = await _repository.GetByUserIdAsync(userId);

            return items.Select(w => new WishlistedItemDto(
                w.Id,
                w.UserId,
                w.VinylId)).ToList();
        }

        public async Task RemoveItemAsync(int wishelistedItemId)
        {
            var item = await _repository.GetWishlistByIdAsync(wishelistedItemId);

            if (item == null)
                throw new KeyNotFoundException($"Wishlisted Item With the Id of {wishelistedItemId} was not found");

            _repository.Remove(item);

            await _unitOfWork.SaveChangesAsync();
        }

        
    }
}
