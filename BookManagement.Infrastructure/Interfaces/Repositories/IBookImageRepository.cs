using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IBookImageRepository
    {
        Task AddAsync(BookImage bookImage);
        Task<IEnumerable<BookImage>> GetAllAsync();
        Task<BookImage?> GetByIdAsync(int imageId);
        Task DeleteAsync(int imageId);
        Task<bool> SaveChangesAsync();

    }
}
