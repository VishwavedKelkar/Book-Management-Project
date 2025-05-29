
using BookManagement.BL.DTOs.BookImageDTOs;

namespace BookManagement.BL.Interfaces.Services.BookImageInterface
{
    public interface IUpdateBookImageService
    {
        Task<BookImageResponse> UpdateBookImageAsync(int id, UpdateBookImageRequest request);
    }
}
