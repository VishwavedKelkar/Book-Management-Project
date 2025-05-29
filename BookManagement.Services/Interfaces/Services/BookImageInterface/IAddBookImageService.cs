
using BookManagement.BL.DTOs.BookImageDTOs;

namespace BookManagement.BL.Interfaces.Services.BookImageInterface
{
    public interface IAddBookImageService
    {
        Task<BookImageResponse> AddBookImageAsync(CreateBookImageRequest request);  
    }
}
