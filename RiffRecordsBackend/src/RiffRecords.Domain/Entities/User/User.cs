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
        public string Username { get; private set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;

        public User(string email, string username, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email can not be blank");

            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Username can not be blank");

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Password can not be blank");

            Email = email;
            Username = username;
            PasswordHash = passwordHash;
        }
    }
}
