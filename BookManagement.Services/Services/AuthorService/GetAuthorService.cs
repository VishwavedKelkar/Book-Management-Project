using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.Core.DTOs.AuthorDTOs;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.AuthorService
{
    public class GetAuthorService : IGetAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<AuthorResponse?> GetAuthorByIdAsync(int authorId)
        {
            try
            {
                var author = await _authorRepository.GetByIdAsync(authorId);
                if (author == null) return null;

                return new AuthorResponse
                {
                    AuthorId = author.AuthorId,
                    Name = author.AuthorName,
                    Bio = author.Bio,
                    DateOfBirth = author.DateOfBirth
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving author with ID {authorId}: {ex.Message}");
                throw new Exception("An error occurred while retrieving the author.");
            }
        }
    }
}
