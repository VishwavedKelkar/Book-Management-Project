using BookManagement.BL.DTOs.BookImageDTOs;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookImageServices
{
    public class AddBookImageService : IAddBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;

        public AddBookImageService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<BookImageResponse> AddBookImageAsync(CreateBookImageRequest request)
        {
            var bookImage = new BookImage
            {
                ImageUrl = request.ImageUrl,
                BookId = request.BookId
            };

            await _bookImageRepository.AddAsync(bookImage);
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
