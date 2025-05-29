using BookManagement.BL.DTOs.GenreDTOs;

namespace BookManagement.BL.Interfaces.Services.GenreInterface
{
    public interface IUpdateGenreService
    {
        Task UpdateGenreAsync(int genreId, UpdateGenreRequest request);
    }
}
