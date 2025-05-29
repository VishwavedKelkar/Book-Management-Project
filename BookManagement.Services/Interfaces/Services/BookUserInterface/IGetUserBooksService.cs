using BookManagement.BL.DTOs.BookUserDTOs;

namespace BookManagement.BL.Interfaces.Services.BookUserInterface
{
    public interface IGetUserBooksService
    {
        Task<IEnumerable<BookAssignmentResponse>> GetBooksByUserIdAsync(int userId);
    }
}
