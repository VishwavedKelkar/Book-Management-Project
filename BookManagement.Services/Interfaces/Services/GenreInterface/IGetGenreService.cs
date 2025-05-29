using BookManagement.BL.DTOs.GenreDTOs;

namespace BookManagement.BL.Interfaces.Services.GenreInterface
{
    public interface IGetGenreService
    {
        Task<GenreResponse?> GetGenreByIdAsync(int genreId);
    }
}
