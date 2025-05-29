using BookManagement.BL.DTOs.BookUserDTOs;
using BookManagement.BL.Interfaces.Services.BookUserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookUserServices
{
    public class GetUserBooksService : IGetUserBooksService
    {
        private readonly IBookUserRepository _bookUserRepository;

        public GetUserBooksService(IBookUserRepository bookUserRepository)
        {
            _bookUserRepository = bookUserRepository;
        }

        public async Task<IEnumerable<BookAssignmentResponse>> GetBooksByUserIdAsync(int userId)
        {
            var entities = await _bookUserRepository.GetBooksByUserIdAsync(userId);

            return entities.Select(e => new BookAssignmentResponse
            {
                UserId = e.UserId,
                Username = e.User?.Username, 
                BookId = e.BookId,
                BookTitle = e.Book?.Title
            });
        }
    }
}
