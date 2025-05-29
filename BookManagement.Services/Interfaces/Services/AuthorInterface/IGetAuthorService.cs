using BookManagement.Core.DTOs.AuthorDTOs;

namespace BookManagement.BL.Interfaces.Services.AuthorInterface
{
    public interface IGetAuthorService
    {
        Task<AuthorResponse?> GetAuthorByIdAsync(int authorId);
    }
}
