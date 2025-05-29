
using BookManagement.BL.DTOs.BookImageDTOs;

namespace BookManagement.BL.Interfaces.Services.BookImageInterface
{
    public interface IGetAllBookImagesService
    {
        Task<IEnumerable<BookImageResponse>> GetAllBookImagesAsync();
    }
}
