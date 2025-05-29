using BookManagement.BL.DTOs.BookImageDTOs;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookImageServices
{
    public class UpdateBookImageService : IUpdateBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;

        public UpdateBookImageService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<BookImageResponse> UpdateBookImageAsync(int id, UpdateBookImageRequest request)
        {
            var bookImage = await _bookImageRepository.GetByIdAsync(id);
            if (bookImage == null) return null;

            bookImage.ImageUrl = request.ImageUrl;


            await _bookImageRepository.SaveChangesAsync();

            return new BookImageResponse
            {
                BookImageId = bookImage.BookImageId,
                ImageUrl = bookImage.ImageUrl,
                BookId = bookImage.BookId
            };
        }
    }
}
