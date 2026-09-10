using RiffRecords.Application.Abstractions;
using RiffRecords.Application.Dtos;
using RiffRecords.Domain.Entities.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RiffRecords.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHash _passwordHash;

        public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IPasswordHash passwordHash)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _passwordHash = passwordHash;
        }

        public async Task<UserDto> CreateNewUserAsync(RegisterUserDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);
            if (existing != null)
                throw new InvalidOperationException("Email is already registered");

            var passwordHash =  _passwordHash.Hash(dto.Password);
            var user = new User(dto.Email, dto.Username, passwordHash);

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return new UserDto(user.Id, user.Email, user.Username, user.Role);
        }


        public async Task<UserDto?> GetUserByEmailAsync(string email)
        {
            var user = await _userRepository.GetByEmailAsync(email);

            if(user == null)
                return null;

            return new UserDto(user.Id, user.Email, user.Username, user.Role);
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserDto(user.Id, user.Email, user.Username, user.Role);
        }

        public async Task<UserDto?> LoginUserAsync(LoginUserDto dto)
        {
            var user = await _userRepository.GetByEmailAsync(dto.Email);

            if (user == null)
                return null;

            var isValid = _passwordHash.Verify(dto.Password, user.PasswordHash);

            if (!isValid)
                return null;

            return new UserDto(user.Id, user.Email, user.Username, user.Role);
        }
    }
}
