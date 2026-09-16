using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities.Band;
using RiffRecords.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Repositories
{
    public class BandRepository : IBandRepository
    {
        private readonly AppDbContext _context;

        public BandRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Band band)
        {
            await _context.Bands.AddAsync(band);
        }

        public void Delete(Band band)
        {
            _context.Bands.Remove(band);
        }

        public async Task<List<Band>> GetAllBandsAsync()
        {
            return await _context.Bands
                .ToListAsync();
        }

        public async Task<Band?> GetBandByIdAsync(int id)
        {
            return await _context.Bands
                .FirstOrDefaultAsync(b => b.BandId == id);
        }

        public void Update(Band band)
        {
            _context.Bands.Update(band);
        }
    }
}
