using System.Threading.Tasks;
using RiffRecords.Domain.Entities.User;

namespace RiffRecords.Application.Abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(int id);
        Task<User?> GetByEmailAsync(string email);
        Task AddAsync(User user);
        void Update(User user);
    }
}