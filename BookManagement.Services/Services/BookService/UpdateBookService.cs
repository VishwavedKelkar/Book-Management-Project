using BookManagement.BL.DTOs.BookDTOs;
using BookManagement.BL.Interfaces.Services.BookInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.BookService
{
    public class UpdateBookService : IUpdateBookService
    {
        private readonly IBookRepository _bookRepository;

        public UpdateBookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task UpdateBookAsync(int bookId, UpdateBookRequest request)
        {
            var existingBook = await _bookRepository.GetByIdAsync(bookId);
            if (existingBook == null)
                throw new InvalidOperationException("Book not found.");

            existingBook.Title = request.Title;
            existingBook.ISBN = request.ISBN;
            existingBook.PublishedDate = request.PublishedDate;
            existingBook.Description = request.Description;
            existingBook.PageCount = request.PageCount;
            existingBook.AuthorId = request.AuthorId;
            existingBook.GenreId = request.GenreId;
            existingBook.UpdatedBy = request.UpdatedBy;
            existingBook.UpdatedDate = DateTime.UtcNow;

            await _bookRepository.UpdateAsync(bookId, existingBook);
        }

    }
}
