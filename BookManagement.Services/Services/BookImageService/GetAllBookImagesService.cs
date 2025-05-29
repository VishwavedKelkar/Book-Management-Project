using BookManagement.BL.DTOs.BookImageDTOs;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.DAL.Interfaces.Repositories;


namespace BookManagement.BL.Services.BookImageServices
{
    public class GetAllBookImagesService : IGetAllBookImagesService
    {
        private readonly IBookImageRepository _bookImageRepository;

        public GetAllBookImagesService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<IEnumerable<BookImageResponse>> GetAllBookImagesAsync()
        {
            var images = await _bookImageRepository.GetAllAsync();

            return images.Select(img => new BookImageResponse
            {
                BookImageId = img.BookImageId,
                ImageUrl = img.ImageUrl,
                BookId = img.BookId
            });
        }
    }
}
