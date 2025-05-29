using BookManagement.Core.DTOs.AuthorDTOs;

namespace BookManagement.BL.Interfaces.Services.AuthorInterface
{
    public interface IGetAllAuthorsService
    {
        Task<IEnumerable<AuthorResponse>> GetAllAuthorsAsync();
    }
}
