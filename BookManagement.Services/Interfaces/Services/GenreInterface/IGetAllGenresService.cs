using BookManagement.BL.DTOs.GenreDTOs;

namespace BookManagement.BL.Interfaces.Services.GenreInterface
{
    public interface IGetAllGenresService
    {
        Task<IEnumerable<GenreResponse>> GetAllGenresAsync();
    }
}
