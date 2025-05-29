using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Repositories
{
    public class BookUserRepository : IBookUserRepository
    {
        private readonly AppDbContext _context;

        public BookUserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BookUser bookUser)
        {
            await _context.BookUsers.AddAsync(bookUser);
        }

        public async Task<IEnumerable<BookUser>> GetBooksByUserIdAsync(int userId)
        {
            return await _context.BookUsers
                .Include(bu => bu.Book)
                .Where(bu => bu.UserId == userId)
                .ToListAsync();
        }

        public async Task<BookUser?> GetByCompositeKeyAsync(int userId, int bookId)
        {
            return await _context.BookUsers
                .Include(bu => bu.Book)
                .Include(bu => bu.User)
                .FirstOrDefaultAsync(bu => bu.UserId == userId && bu.BookId == bookId);
        }

        public async Task RemoveAsync(int userId, int bookId)
        {
            var record = await GetByCompositeKeyAsync(userId, bookId);
            if (record != null)
                _context.BookUsers.Remove(record);
        }

        public async Task<bool> ExistsAsync(int userId, int bookId)
        {
            return await _context.BookUsers
                .AnyAsync(bu => bu.UserId == userId && bu.BookId == bookId);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
