using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IBookUserRepository
    {
        Task AddAsync(BookUser bookUser);
        Task<IEnumerable<BookUser>> GetBooksByUserIdAsync(int userId);
        Task<BookUser?> GetByCompositeKeyAsync(int userId, int bookId);
        Task RemoveAsync(int userId, int bookId);
        Task<bool> ExistsAsync(int userId, int bookId);
        Task<bool> SaveChangesAsync();
    }
}
