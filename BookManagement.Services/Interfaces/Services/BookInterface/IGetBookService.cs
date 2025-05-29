using BookManagement.BL.DTOs.BookDTOs;

namespace BookManagement.BL.Interfaces.Services.BookInterface
{
    public interface IGetBookService
    {
        Task<BookResponse> GetBookByIdAsync(int bookId);
    }
}
