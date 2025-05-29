using BookManagement.BL.DTOs.BookDTOs;
using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookService
{
    public class CreateBookService : ICreateBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;
        private readonly IGenreRepository _genreRepository;

        public CreateBookService(
            IBookRepository bookRepository,
            IAuthorRepository authorRepository,
            IGenreRepository genreRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
            _genreRepository = genreRepository;
        }

        public async Task<BookResponse?> CreateBookAsync(CreateBookRequest request)
        {
            var author = await _authorRepository.GetByIdAsync(request.AuthorId);
            var genre = await _genreRepository.GetByIdAsync(request.GenreId);

            if (author == null || genre == null)
                return null;

            var book = new Book
            {
                Title = request.Title,
                ISBN = request.ISBN,
                PublishedDate = request.PublishedDate,
                Description = request.Description,
                PageCount = request.PageCount,
                AuthorId = request.AuthorId,
                GenreId = request.GenreId,
                CreatedBy = request.CreatedBy
            };

            await _bookRepository.CreateAsync(book);
            await _bookRepository.SaveChangesAsync();

            return new BookResponse
            {
                BookId = book.BookId,
                Title = book.Title,
                ISBN = book.ISBN,
                PublishedDate = book.PublishedDate,
                Description = book.Description,
                PageCount = book.PageCount,
                AuthorId = author.AuthorId,
                AuthorName = author.AuthorName,
                GenreId = genre.GenreId,
                GenreName = genre.GenreName
            };
        }
    }
}
