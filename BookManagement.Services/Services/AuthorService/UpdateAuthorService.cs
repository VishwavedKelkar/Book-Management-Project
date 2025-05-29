using BookManagement.BL.DTOs.AuthorDTOs;
using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.AuthorService
{
    public class UpdateAuthorService : IUpdateAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public UpdateAuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<bool> UpdateAuthorAsync(int authorId, UpdateAuthorRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Update request cannot be null.");

            if (string.IsNullOrWhiteSpace(request.AuthorName))
                throw new ArgumentException("Author name is required.", nameof(request.AuthorName));

            try
            {
                var existingAuthor = await _authorRepository.GetByIdAsync(authorId);
                if (existingAuthor == null)
                    return false;

                existingAuthor.AuthorName = request.AuthorName;
                existingAuthor.Bio = request.Bio;
                existingAuthor.DateOfBirth = request.DateOfBirth;

                await _authorRepository.UpdateAsync(authorId, existingAuthor);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating author with ID {authorId}: {ex.Message}");
                throw new Exception("An error occurred while updating the author.");
            }
        }
    }
}
