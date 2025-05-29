using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookManagement.DAL.Repositories
{
    public class BookImageRepository : IBookImageRepository
    {
        private readonly AppDbContext _context;

        public BookImageRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BookImage bookImage)
        {
            await _context.BookImages.AddAsync(bookImage);
        }

        public async Task<IEnumerable<BookImage>> GetAllAsync()
        {
            return await _context.BookImages
                                 .Include(bi => bi.Book) 
                                 .ToListAsync();
        }

        public async Task<BookImage?> GetByIdAsync(int imageId)
        {
            return await _context.BookImages
                                 .Include(bi => bi.Book) 
                                 .FirstOrDefaultAsync(bi => bi.BookImageId == imageId);
        }

        public async Task DeleteAsync(int imageId)
        {
            var bookImage = await _context.BookImages.FindAsync(imageId);
            if (bookImage != null)
            {
                _context.BookImages.Remove(bookImage);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
    }
}
