using RiffRecords.Application.Options;
using RiffRecords.Application.Abstractions;


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
            AdminSeeder)
    }
}
