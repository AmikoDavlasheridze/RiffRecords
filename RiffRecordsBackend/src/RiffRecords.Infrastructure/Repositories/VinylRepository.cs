using Microsoft.EntityFrameworkCore;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities.Band;
using RiffRecords.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Repositories
{
    public class VinylRepository : IVinylRepository
    {
        private readonly AppDbContext _context;

        public VinylRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Vinyl vinyl)
        {
            await _context.Vinyls.AddAsync(vinyl);
        }

        public void Delete(Vinyl vinyl)
        {
             _context.Vinyls.Remove(vinyl);
        }

        public async Task<List<Vinyl>> GetAllAsync()
        {
            return await _context.Vinyls
                .Include(v => v.Band)
                .ToListAsync();
        }

        public async Task<List<Vinyl>> GetByBandIdAsync(int bandId)
        {
            return await _context.Vinyls
                .Include(v => v.Band)
                .Where(v => v.BandId == bandId)
                .ToListAsync();

        }

        public async Task<Vinyl?> GetByIdAsync(int id)
        {
            return await _context.Vinyls
                .Include(v => v.Band)
                .FirstOrDefaultAsync(v => v.VinylId == id);
        }

        public void Update(Vinyl vinyl)
        {
            _context.Vinyls.Update(vinyl);
        }
    }
}
