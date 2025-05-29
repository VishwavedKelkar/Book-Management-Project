using BookManagement.BL.DTOs.GenreDTOs;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.GenreService
{
    public class GetAllGenresService : IGetAllGenresService
    {
        private readonly IGenreRepository _genreRepository;

        public GetAllGenresService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<IEnumerable<GenreResponse>> GetAllGenresAsync()
        {
            var genres = await _genreRepository.GetAllAsync();

            return genres.Select(g => new GenreResponse
            {
                GenreId = g.GenreId,
                GenreName = g.GenreName
            });
        }
    }
}
