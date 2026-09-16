using Microsoft.EntityFrameworkCore;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities.User;
using RiffRecords.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.Users.AddAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Update(User user)
        {
            _context.Users.Update(user);
        }
    }
}
