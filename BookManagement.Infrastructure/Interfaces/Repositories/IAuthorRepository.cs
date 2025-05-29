using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IAuthorRepository
    {
        Task CreateAsync(Author author);
        Task<Author?> GetByIdAsync(int authorId);
        Task<IEnumerable<Author>> GetAllAsync();
        Task UpdateAsync(int authorId,Author author); 
        Task DeleteAsync(int authorId);

    }
}
