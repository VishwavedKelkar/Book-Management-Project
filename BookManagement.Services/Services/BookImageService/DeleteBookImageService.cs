using BookManagement.BL.Interfaces.Services.BookImageInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookImageServices
{
    public class DeleteBookImageService : IDeleteBookImageService
    {
        private readonly IBookImageRepository _bookImageRepository;

        public DeleteBookImageService(IBookImageRepository bookImageRepository)
        {
            _bookImageRepository = bookImageRepository;
        }

        public async Task<bool> DeleteBookImageAsync(int id)
        {
            var bookImage = await _bookImageRepository.GetByIdAsync(id);
            if (bookImage == null) return false;

            await _bookImageRepository.DeleteAsync(id);
            await _bookImageRepository.SaveChangesAsync();
            return true;
        }
    }
}
