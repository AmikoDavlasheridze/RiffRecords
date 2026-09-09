using System;
using System.Collections.Generic;
using System.Text;
using RiffRecords.Domain.Entities.Band;

namespace RiffRecords.Application.Abstractions
{
    public interface IVinylRepository
    {
        Task<Vinyl?> GetByIdAsync(int id);
        Task<List<Vinyl>> GetByBandIdAsync(int bandId);
        Task<List<Vinyl>> GetAllAsync();
        Task AddAsync(Vinyl vinyl);
        void Update(Vinyl vinyl);
        void Delete(Vinyl vinyl);

    }
}
