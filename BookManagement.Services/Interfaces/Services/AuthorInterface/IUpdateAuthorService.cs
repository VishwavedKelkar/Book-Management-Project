using BookManagement.BL.DTOs.AuthorDTOs;
using BookManagement.Core.Entities;

namespace BookManagement.BL.Interfaces.Services.AuthorInterface
{
    public interface IUpdateAuthorService
    {
        Task<bool> UpdateAuthorAsync(int authorId, UpdateAuthorRequest request);
    }
}
