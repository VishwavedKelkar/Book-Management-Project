
using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IBookRepository
    {
        Task CreateAsync(Book book);
        Task<Book?> GetByIdAsync(int bookId);
        Task<IEnumerable<Book>> GetAllAsync();
        Task UpdateAsync(int bookId, Book updatedBook);
        Task DeleteAsync(int bookId);
        Task SaveChangesAsync();
    }
}
