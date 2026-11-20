using RiffRecords.Application.Options;
using RiffRecords.Application.Abstractions;
using RiffRecords.Domain.Entities;
using RiffRecords.Domain.Entities.User;
using RiffRecords.Domain.Enum;


namespace RiffRecords.Application.Services
{
    public sealed class AdminSeeder
    {
        private readonly IUserRepository _userRepository;
        private readonly  IPasswordHash _passwordHash;
        private readonly IUnitOfWork _unitOfWork;

        public AdminSeeder(IUserRepository userRepository, IPasswordHash passwordHash, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _passwordHash = passwordHash;
            _unitOfWork = unitOfWork;
        }

        public async Task SeedAsync (
            AdminSeedOptions options, CancellationToken ct = default)
        {
            var user = await _userRepository.GetByEmailAsync(options.Email);

            if (user is null)
            {
                user = new User(options.Email, options.Username, _passwordHash.Hash(options.Password));
                await _userRepository.AddAsync(user);
            }

            if (user.Role != UserRole.Admin)
                user.PromoteToAdmin();

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
