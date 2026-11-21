using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Abstractions.serviceAbstractions;
using RiffRecords.Application.Dtos;
using RiffRecords.Domain.Entities.User;

namespace RiffRecords.Application.Services
{
    public class WishlistedItemService : IWishlistedItemService
    {
        private readonly IWishlistedItemRepository _repository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVinylRepository _vinylRepository;

        public WishlistedItemService(IWishlistedItemRepository repository, IUnitOfWork unitOfWork, IVinylRepository vinylRepository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
            _vinylRepository = vinylRepository;
        }

        public async Task<WishlistedItemDto> AddAnItemAsync(int userId, AddWishlistItemDto dto)
        {
            if (await _vinylRepository.GetByIdAsync(dto.VinylId) == null)
                throw new KeyNotFoundException($"Vinyl {dto.VinylId} was not found");

            if (await _repository.GetByUserAndVinylAsync(userId, dto.VinylId) != null)
                throw new InvalidOperationException("This vinyl is already in the wishlist");

            var item = new WishlistedItem { UserId = userId, VinylId = dto.VinylId };

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

        public async Task RemoveItemAsync(int userId, int wishlistedItemId)
        {
            var item = await _repository.GetWishlistByIdAsync(wishlistedItemId);

            if (item == null || item.UserId != userId)
                throw new KeyNotFoundException($"Wishlisted item {wishlistedItemId} was not found");

            _repository.Remove(item);
            await _unitOfWork.SaveChangesAsync();
        }
    }

}
