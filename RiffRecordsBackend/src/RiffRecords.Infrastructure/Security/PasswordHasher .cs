using Microsoft.AspNetCore.Identity;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities.User;
using RiffRecords.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHash
    {
        private readonly PasswordHasher<User> _hasher = new();


        public string Hash(string password)
        {
            return _hasher.HashPassword(null!, password);
        }

        public bool Verify(string password, string passwordHash)
        {
            var res = _hasher.VerifyHashedPassword(null!, passwordHash, password);
            return res == PasswordVerificationResult.Success;
        }
    }
}
