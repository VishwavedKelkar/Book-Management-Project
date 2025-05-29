
using BookManagement.BL.DTOs.GenreDTOs;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.GenreService
{
    public class CreateGenreService : ICreateGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public CreateGenreService(IGenreRepository genreRepository) {
            _genreRepository = genreRepository;
        }

        public async Task<GenreResponse> CreateGenreAsync(CreateGenreRequest request) {
            if (string.IsNullOrWhiteSpace(request.GenreName))
                throw new ArgumentException("Genre name is required.");

            var genre = new Genre
            {
                GenreName = request.GenreName,
            };

            await _genreRepository.CreateAsync(genre);

            return new GenreResponse
            {
                GenreId = genre.GenreId,
                GenreName = genre.GenreName,
            };
        }
    }
}
