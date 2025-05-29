using BookManagement.BL.DTOs.BookDTOs;

namespace BookManagement.BL.Interfaces.Services.BookInterface
{
    public interface IGetAllBooksService
    {
        Task<IEnumerable<BookResponse>> GetAllBooksAsync();
    }
}
