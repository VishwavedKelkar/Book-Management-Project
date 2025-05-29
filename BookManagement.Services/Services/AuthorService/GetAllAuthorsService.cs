using BookManagement.BL.Interfaces.Services.AuthorInterface;
using BookManagement.Core.DTOs.AuthorDTOs;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.AuthorService
{
    public class GetAllAuthorsService : IGetAllAuthorsService
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAllAuthorsService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<IEnumerable<AuthorResponse>> GetAllAuthorsAsync()
        {
            try
            {
                var authors = await _authorRepository.GetAllAsync();

                return authors.Select(author => new AuthorResponse
                {
                    AuthorId = author.AuthorId,
                    Name = author.AuthorName,
                    Bio = author.Bio,
                    DateOfBirth = author.DateOfBirth
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching authors: {ex.Message}");
                throw new Exception("An error occurred while retrieving authors.");
            }
        }
    }
}
