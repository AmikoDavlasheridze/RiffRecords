using RiffRecords.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Abstractions
{
    public interface IUserService
    {
        Task<UserDto?> GetUserByIdAsync (int id);
        Task<UserDto?> GetUserByEmailAsync (string email);
        Task<UserDto?> LoginUserAsync(LoginUserDto dto);
        Task<UserDto> CreateNewUserAsync(RegisterUserDto dto);

    }
}
