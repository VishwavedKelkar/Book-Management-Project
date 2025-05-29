using BookManagement.BL.DTOs.BookDTOs;
using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookService
{
    public class GetAllBooksService : IGetAllBooksService
    {
        private readonly IBookRepository _bookRepository;

        public GetAllBooksService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookResponse>> GetAllBooksAsync()
        {
            var books = await _bookRepository.GetAllAsync();

            return books.Select(b => new BookResponse
            {
                BookId = b.BookId,
                Title = b.Title,
                ISBN = b.ISBN,
                PublishedDate = b.PublishedDate,
                Description = b.Description,
                PageCount = b.PageCount,
                AuthorId = b.AuthorId,
                AuthorName = b.Author?.AuthorName ?? "Unknown",
                GenreId = b.GenreId,
                GenreName = b.Genres?.GenreName ?? "Unknown"
            });
        }
    }
}
