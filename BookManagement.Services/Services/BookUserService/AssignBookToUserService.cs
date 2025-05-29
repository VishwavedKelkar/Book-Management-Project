using BookManagement.BL.DTOs.BookUserDTOs;
using BookManagement.BL.Interfaces.Services.BookUserInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookUserServices
{
    public class AssignBookToUserService : IAssignBookToUserService
    {
        private readonly IBookUserRepository _bookUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;
        public AssignBookToUserService(
            IBookUserRepository bookUserRepository,
            IUserRepository userRepository,
            IBookRepository bookRepository)
        {
            _bookUserRepository = bookUserRepository;
            _userRepository = userRepository;
            _bookRepository = bookRepository;
        }

        public async Task<BookAssignmentResponse?> AssignAsync(AssignBookToUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId);
            var book = await _bookRepository.GetByIdAsync(request.BookId);

            if (user == null || book == null)
                return null;

            var entity = new BookUser
            {
                UserId = request.UserId,
                BookId = request.BookId
            };

            await _bookUserRepository.AddAsync(entity);
            await _bookUserRepository.SaveChangesAsync();

            return new BookAssignmentResponse
            {
                UserId = request.UserId,
                Username = user.Username,
                BookId = request.BookId,
                BookTitle = book.Title
            };
        }
    }
}
