using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.GenreService
{
    public class DeleteGenreService : IDeleteGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public DeleteGenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task DeleteGenreAsync(int genreId)
        {
            await _genreRepository.DeleteAsync(genreId);
        }
    }
}
