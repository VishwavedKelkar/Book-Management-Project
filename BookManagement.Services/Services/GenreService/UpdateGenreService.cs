using BookManagement.BL.DTOs.GenreDTOs;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.GenreService
{
    public class UpdateGenreService : IUpdateGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public UpdateGenreService(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        public async Task UpdateGenreAsync(int genreId, UpdateGenreRequest request)
        {
            var updatedGenre = new Genre
            {
                GenreId = genreId,
                GenreName = request.GenreName
            };
            await _genreRepository.UpdateAsync(genreId, updatedGenre);
        }
    }
}
