using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookService
{
    public class DeleteBookService : IDeleteBookService
    {
        private readonly IBookRepository _bookRepository;

        public DeleteBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            await _bookRepository.DeleteAsync(bookId);
        }
    }
}
