using BookManagement.BL.DTOs.GenreDTOs;


namespace BookManagement.BL.Interfaces.Services.GenreInterface
{
    public interface ICreateGenreService
    {
        Task<GenreResponse> CreateGenreAsync(CreateGenreRequest request);
    }
}
