using RiffRecords.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Domain.Entities.User
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
    }
}
