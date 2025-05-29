using BookManagement.Core.Entities;
using BookManagement.Core.DTOs.AuthorDTOs;
using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.AuthorService
{
    public class CreateAuthorService : ICreateAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public CreateAuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorResponse> CreateAuthorAsync(CreateAuthorRequest createAuthorRequest)
        {
            if (createAuthorRequest == null)
                throw new ArgumentNullException(nameof(createAuthorRequest), "Author request cannot be null.");

            if (string.IsNullOrWhiteSpace(createAuthorRequest.AuthorName))
                throw new ArgumentException("Author name is required.", nameof(createAuthorRequest.AuthorName));

            if (createAuthorRequest.DateOfBirth == default)
                throw new ArgumentException("Valid Date of Birth is required.", nameof(createAuthorRequest.DateOfBirth));

            try
            {
                var author = new Author
                {
                    AuthorName = createAuthorRequest.AuthorName,
                    Bio = createAuthorRequest.Bio,
                    DateOfBirth = createAuthorRequest.DateOfBirth
                };

                await _authorRepository.CreateAsync(author);

                return new AuthorResponse
                {
                    AuthorId = author.AuthorId,
                    Name = author.AuthorName,
                    Bio = author.Bio,
                    DateOfBirth = author.DateOfBirth
                };
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Argument Exception: {ex.Message}");
                throw new Exception($"Argument Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while creating author: {ex.Message}");
                throw new Exception($"An error occurred while creating the author: {ex.Message}");
            }
        }
    }
}
