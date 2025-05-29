using BookManagement.Core.Entities;

namespace BookManagement.DAL.Interfaces.Repositories
{
    public interface IGenreRepository
    {
        Task CreateAsync(Genre genre);
        Task<Genre?> GetByIdAsync(int genreId);
        Task<IEnumerable<Genre>> GetAllAsync();
        Task UpdateAsync(int genreId, Genre updatedGenre);
        Task DeleteAsync(int genreId);
    }
}
