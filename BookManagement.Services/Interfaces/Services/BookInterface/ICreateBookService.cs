using BookManagement.BL.DTOs.BookDTOs;

namespace BookManagement.BL.Interfaces.Services.BookInterface
{
    public interface ICreateBookService
    {
        Task<BookResponse?> CreateBookAsync(CreateBookRequest request);
    }
}
