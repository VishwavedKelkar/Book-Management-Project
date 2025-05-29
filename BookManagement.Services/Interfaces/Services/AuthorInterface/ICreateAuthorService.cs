using BookManagement.Core.DTOs.AuthorDTOs;
using BookManagement.Core.Entities;

namespace BookManagement.BL.Interfaces.Services.AuthorInterface
{
    public interface ICreateAuthorService
    {
        Task<AuthorResponse> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest);
    }
}
