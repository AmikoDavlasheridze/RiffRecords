using RiffRecords.Application.Dtos;
using RiffRecords.Domain.Entities.Band;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Services
{
    public interface IBandService
    {
        Task<BandDto?> GetBandByIdAsync(int id);
        Task<List<BandDto>> GetAllBandsAsync();
        Task<BandDto> CreateBandAsync(CreateBandDto dto);
        Task UpdateAsync(int bandId, CreateBandDto dto);
        Task DeleteAsync(int bandId);
    }
}
