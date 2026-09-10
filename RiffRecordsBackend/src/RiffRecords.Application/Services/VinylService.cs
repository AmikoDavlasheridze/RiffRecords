using System;
using System.Collections.Generic;
using System.Text;
using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Dtos;
using RiffRecords.Application.Services.Interfaces;
using RiffRecords.Domain.Entities.Band;

namespace RiffRecords.Application.Services
{
    internal class VinylService : IVinylService
    {
        private readonly IVinylRepository _vinylRepository;
        private readonly IUnitOfWork _unitOfWork;

        public VinylService(IVinylRepository vinylRepository, IUnitOfWork unitOfWork)
        {
            vinylRepository = _vinylRepository;
            unitOfWork = _unitOfWork;
        }

        public async Task<VinylDto> CreateAsync(CreateVinylDto dto)
        {
            var vinyl = new Vinyl(
                dto.AlbumTitle,
                dto.Price,
                dto.ReleaseYear,
                dto.Image,
                dto.BandId,
                dto.InitialStock);

            await _vinylRepository.AddAsync(vinyl);
            await _unitOfWork.SaveChangesAsync();

            return new VinylDto(
                    vinyl.VinylId,
                    vinyl.AlbumTitle,
                    vinyl.Price,
                    vinyl.ReleaseYear,
                    vinyl.Image,
                    vinyl.Stock,
                    vinyl.BandId,
                    string.Empty
                    );
        }

        public async Task DecreaseStockAsync(int vinylId, int quantity)
        {
            var vinyl = await _vinylRepository.GetByIdAsync(vinylId);

            if (vinyl == null)
                throw new KeyNotFoundException($"Vinyl with id {vinylId} was not found");

            vinyl.DecreaseStock(quantity);

            await _unitOfWork.SaveChangesAsync();

        }

        public async Task<List<VinylDto>> GetAllAsync()
        {
            var vinyls = await _vinylRepository.GetAllAsync();

            return vinyls.Select(v => new VinylDto(
                v.VinylId,
                v.AlbumTitle,
                v.Price,
                v.ReleaseYear,
                v.Image,
                v.Stock,
                v.BandId,
                v.Band.BandName
            )).ToList();

            
        }

        public async Task<VinylDto?> GetByIdAsync(int id)
        {
            var vinyl = await _vinylRepository.GetByIdAsync(id);

            if(vinyl == null)
                return null;

            return new VinylDto(
                vinyl.VinylId,
                vinyl.AlbumTitle,
                vinyl.Price,
                vinyl.ReleaseYear,
                vinyl.Image,
                vinyl.Stock,
                vinyl.BandId,
                vinyl.Band.BandName
                );
                
        }
    }
}
