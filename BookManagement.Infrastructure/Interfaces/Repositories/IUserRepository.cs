
using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        void Update(User user);
        Task DeleteAsync(int id);
        Task<bool> SaveChangesAsync();

        Task<User?> GetByUsernameAsync(string username);
        Task AddUserRoleAsync(UserRole userRole);

    }
}
