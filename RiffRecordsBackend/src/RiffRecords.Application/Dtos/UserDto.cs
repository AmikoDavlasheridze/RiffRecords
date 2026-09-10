using RiffRecords.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Dtos
{
    public record RegisterUserDto(string Email, string Username, string Password);
    public record LoginUserDto(string Email, string Password);
    public record UserDto(int Id, string Email, string Username, UserRole Role);
}
