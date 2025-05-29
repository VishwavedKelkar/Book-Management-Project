using BookManagement.BL.DTOs.GenreDTOs;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.GenreService
{
    public class GetGenreService : IGetGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GetGenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponse?> GetGenreByIdAsync(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);
            if (genre == null) return null;

            return new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }
    }
}
