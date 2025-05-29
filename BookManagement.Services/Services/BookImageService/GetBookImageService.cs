using BookManagement.BL.DTOs.BookImageDTOs;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookImageServices
{
    public class GetBookImageService : IGetBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;

        public GetBookImageService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<BookImageResponse> GetBookImageByIdAsync(int id)
        {
            var img = await _bookImageRepository.GetByIdAsync(id);

            if (img == null) return null;

            return new BookImageResponse
            {
                BookImageId = img.BookImageId,
                ImageUrl = img.ImageUrl,
                BookId = img.BookId
            };
        }
    }
}
