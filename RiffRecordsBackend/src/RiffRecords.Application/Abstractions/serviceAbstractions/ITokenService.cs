using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions.serviceAbstractions
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
        public record TokenResult(string Token, DateTime ExpiresAt);
    }
}
