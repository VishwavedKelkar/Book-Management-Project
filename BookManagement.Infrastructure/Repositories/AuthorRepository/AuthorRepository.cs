using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Repositories.AuthorRepository
{
    public class AuthorRepository : IAuthorRepository
    {
        private readonly AppDbContext _context;

        public AuthorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Author author)
        {
            await _context.Authors.AddAsync(author);
            await _context.SaveChangesAsync();
        }

        public async Task<Author?> GetByIdAsync(int authorId)
        {
            return await _context.Authors.FindAsync(authorId);
        }

        public async Task<IEnumerable<Author>> GetAllAsync()
        {
            return await _context.Authors.ToListAsync(); 
        }

        public async Task UpdateAsync(int authorId, Author updatedAuthor)
        {
            var existingAuthor = await _context.Authors.FindAsync(authorId);
            if (existingAuthor is null) return;

            existingAuthor.AuthorName = updatedAuthor.AuthorName;
            existingAuthor.Bio = updatedAuthor.Bio;
            existingAuthor.DateOfBirth = updatedAuthor.DateOfBirth;

            _context.Authors.Update(existingAuthor);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int authorId)
        {
            var author = await _context.Authors.FindAsync(authorId);
            if (author is null) return;

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
        }
    }
}
