using RiffRecords.Application.Dtos.RiffRecords.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Services.Interfaces
{
    internal interface IVinylService
    {
        Task<VinylDto?> GetByIdAsync(int id);
        Task<List<VinylDto>> GetAllAsync();
        Task<VinylDto> CreateAsync(CreateVinylDto dto);
        Task DecreaseStockAsync(int vinylId, int quantity);
    }
}
