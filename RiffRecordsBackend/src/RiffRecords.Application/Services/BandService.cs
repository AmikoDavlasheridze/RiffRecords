using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Dtos;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Services
{
    public class BandService : IBandService
    {
        private readonly IBandRepository _bandRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BandService(IBandRepository bandRepository, IUnitOfWork unitOfWork)
        {
            bandRepository = _bandRepository;
            unitOfWork = _unitOfWork;
        }

        public async Task<BandDto> CreateBandAsync(CreateBandDto dto)
        {
            var band = new Band(
                dto.Genre,
                dto.BandName,
                dto.BandPicture
                );

            await _bandRepository.AddAsync(band);
            await _unitOfWork.SaveChangesAsync();

            return new BandDto(
                band.BandId,
                band.BandName,
                band.BandPicture,
                band.Genre,
                band.Slug);
        }

        public async Task DeleteAsync(int bandId)
        {
            var band = await _bandRepository.GetBandByIdAsync(bandId);

            if (band == null)
                throw new KeyNotFoundException($"A Band with the ID of {bandId} does not exist");

            _bandRepository.Delete(band);

            await _unitOfWork.SaveChangesAsync();

            
        }

        public async Task<List<BandDto>> GetAllBandsAsync()
        {
            var bands = await _bandRepository.GetAllBandsAsync();

            return bands.Select(b => new BandDto(
                b.BandId,
                b.BandName,
                b.BandPicture,
                b.Genre,
                b.Slug
                )).ToList();
        }

        public async Task<BandDto?> GetBandByIdAsync(int id)
        {
            var band = await _bandRepository.GetBandByIdAsync(id);

            if (band == null)
                return null;

            return new BandDto(
                band.BandId,
                band.BandName,
                band.BandPicture,
                band.Genre,
                band.Slug
                );
        }

        public async Task UpdateAsync(int bandId, CreateBandDto dto)
        {
            var band = await _bandRepository.GetBandByIdAsync(bandId);

            if (band == null)
                throw new KeyNotFoundException($"A Band with the ID of {bandId} does not exist");

            band.UpdateDetails(dto.BandName, dto.Genre, dto.BandPicture);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
