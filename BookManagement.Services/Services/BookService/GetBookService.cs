using BookManagement.BL.DTOs.BookDTOs;
using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookService
{
    public class GetBookService : IGetBookService
    {
        private readonly IBookRepository _bookRepository;

        public GetBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<BookResponse?> GetBookByIdAsync(int bookId)
        {
            var book = await _bookRepository.GetByIdAsync(bookId);
            if (book == null) return null;

            return new BookResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Description = book.Description,
                PageCount = book.PageCount,
                AuthorId = book.AuthorId,
                AuthorName = book.Author?.AuthorName ?? "Unknown",
                GenreId = book.GenreId,
                GenreName = book.Genres?.GenreName ?? "Unknown"
            };
        }

    }
}
