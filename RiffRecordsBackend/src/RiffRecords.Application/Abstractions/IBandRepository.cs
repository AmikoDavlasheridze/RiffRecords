using System.Collections.Generic;
using System.Threading.Tasks;
using RiffRecords.Domain.Entities.Band;

namespace RiffRecords.Application.Abstractions
{
    public interface IBandRepository
    {
        Task<Band?> GetBandByIdAsync(int id);
        Task<List<Band>> GetAllBandsAsync();
        Task AddAsync(Band band);
        void Update(Band band);
        void Delete(Band band);
    }
}