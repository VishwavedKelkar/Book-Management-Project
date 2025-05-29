using BookManagement.BL.DTOs.BookDTOs;

namespace BookManagement.BL.Interfaces.Services.BookInterface
{
    public interface IUpdateBookService
    {
        Task UpdateBookAsync(int bookId, UpdateBookRequest request);
    }
}
