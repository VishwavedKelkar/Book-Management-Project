using BookManagement.BL.Interfaces.Services.BookUserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookUserServices
{
    public class RemoveBookFromUserService : IRemoveBookFromUserService
    {
        private readonly IBookUserRepository _bookUserRepository;

        public RemoveBookFromUserService(IBookUserRepository bookUserRepository)
        {
            _bookUserRepository = bookUserRepository;
        }

        public async Task<bool> RemoveAsync(int userId, int bookId)
        {
            var entity = await _bookUserRepository.GetByCompositeKeyAsync(userId, bookId);
            if (entity == null) return false;

            await _bookUserRepository.RemoveAsync(userId, bookId);
            return await _bookUserRepository.SaveChangesAsync();
        }
    }
}
