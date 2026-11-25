using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Abstractions.serviceAbstractions;
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
            _bandRepository = bandRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BandDto> CreateBandAsync(CreateBandDto dto)
        {
            var band = new Band(
                dto.Genre,
                dto.BandName,
                dto.BandPicture
                );
            
            if (await _bandRepository.SlugExistsAsync(band.Slug))          
                throw new InvalidOperationException($"A band named '{dto.BandName}' already exists.");


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

        public async Task<BandDetailsDto?> GetBandByIdAsync(int id)
        {
            var band = await _bandRepository.GetBandWithDetailsAsync(id);

            if (band == null)
                return null;

            return new BandDetailsDto(
                band.BandId,
                band.BandName,
                band.BandPicture,
                band.Genre,
                band.Slug,
                band.Bio?.BioText,
                band.Bio?.HistoryText,
                band.Achievements
                    .OrderBy(a => a.Id)
                    .Select(a => a.Description)
                    .ToList(),
                band.Vinyls
                    .OrderBy(v => v.ReleaseYear)
                    .Select(v => new BandVinylDto(
                        v.VinylId,
                        v.AlbumTitle,
                        v.Price,
                        v.ReleaseYear,
                        v.Image,
                        v.Stock,
                        v.Description,
                        v.Label,
                        v.RuntimeSeconds,
                        v.Tracks
                            .OrderBy(t => t.TrackNumber)
                            .Select(t => new TrackDto(
                                t.TrackNumber,
                                t.TrackName,
                                t.DurationSeconds,
                                t.IsFamous))
                            .ToList()))
                    .ToList()
                );
        }

        public async Task UpdateAsync(int bandId, CreateBandDto dto)
        {
            var band = await _bandRepository.GetBandByIdAsync(bandId);

            if (band == null)
                throw new KeyNotFoundException($"A Band with the ID of {bandId} does not exist");

            band.UpdateDetails(dto.BandName, dto.Genre, dto.BandPicture);

            if (await _bandRepository.SlugExistsAsync(band.Slug, bandId))  
                throw new InvalidOperationException($"A band named '{dto.BandName}' already exists.");

            await _unitOfWork.SaveChangesAsync();

            
        }
    }
}
