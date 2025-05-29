using BookManagement.BL.DTOs.BookUserDTOs;

namespace BookManagement.BL.Interfaces.Services.BookUserInterface
{
    public interface IAssignBookToUserService
    {
        Task<BookAssignmentResponse?> AssignAsync(AssignBookToUserRequest request);
    }
}
