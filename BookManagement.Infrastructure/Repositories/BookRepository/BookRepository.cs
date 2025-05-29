using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Repositories.BookRepository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;

        public BookRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Book book)
        {
            await _context.Books.AddAsync(book);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int bookId)
        {
            return await _context.Books
                .Include(b => b.Author)
                .Include(b => b.Genres)
                .FirstOrDefaultAsync(b => b.BookId == bookId);
        }

        public async Task UpdateAsync(int bookId, Book updatedBook)
        {
            var existingBook = await _context.Books.FindAsync(bookId);
            if (existingBook == null)
                return;

            // Check for duplicate ISBN
            if (!string.IsNullOrWhiteSpace(updatedBook.ISBN))
            {
                var isbnExists = await _context.Books
                    .AnyAsync(b => b.ISBN == updatedBook.ISBN && b.BookId != bookId);
                if (isbnExists)
                {
                    throw new InvalidOperationException("Another book with the same ISBN already exists.");
                }
            }

            // Update all properties at once
            _context.Entry(existingBook).CurrentValues.SetValues(updatedBook);
            existingBook.UpdatedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int bookId)
        {
            var book = await _context.Books.FindAsync(bookId);
            if (book is null) return;

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync(); 
        }

    }
}
