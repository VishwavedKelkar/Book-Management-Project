using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.AuthorService
{
    public class DeleteAuthorService : IDeleteAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public DeleteAuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> DeleteAuthorAsync(int authorId)
        {
            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(authorId);
                if (existingAuthor == null)
                    return false;

                await _authorRepository.DeleteAsync(authorId);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting author with ID {authorId}: {ex.Message}");
                throw new Exception("An error occurred while deleting the author.");
            }
        }
    }
}
