using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Repositories.GenreRepository
{
    public class GenreRepository : IGenreRepository
    {
        private readonly AppDbContext _context;

        public GenreRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Genre genre)
        {
            await _context.Genres.AddAsync(genre);
            await _context.SaveChangesAsync();
        }

        public async Task<Genre?> GetByIdAsync(int genreId)
        {
            return await _context.Genres.FindAsync(genreId);
        }

        public async Task<IEnumerable<Genre>> GetAllAsync()
        {
            return await _context.Genres.ToListAsync();
        }

        public async Task UpdateAsync(int genreId, Genre updatedGenre)
        {
            var existingGenre = await _context.Genres.FindAsync(genreId);
            if (existingGenre is null) return;

            existingGenre.GenreName = updatedGenre.GenreName;

            _context.Genres.Update(existingGenre);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int genreId)
        {
            var genre = await _context.Genres.FindAsync(genreId);
            if (genre is null) return;

            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
    }
}
