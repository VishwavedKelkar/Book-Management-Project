
using BookManagement.BL.DTOs.BookImageDTOs;

namespace BookManagement.BL.Interfaces.Services.BookImageInterface
{
    public interface IGetBookImageService
    {
        Task<BookImageResponse> GetBookImageByIdAsync(int id);
    }
}
