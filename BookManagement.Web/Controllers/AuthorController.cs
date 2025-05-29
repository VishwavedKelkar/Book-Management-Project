using BookManagement.Core.DTOs.AuthorDTOs;
using BookManagement.BL.Interfaces.Services.AuthorInterface;
using Microsoft.AspNetCore.Mvc;
using BookManagement.BL.DTOs.AuthorDTOs;
using Microsoft.AspNetCore.Authorization;

namespace BookManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICreateAuthorService _createAuthorService;
        private readonly IGetAuthorService _getAuthorService;
        private readonly IGetAllAuthorsService _getAllAuthorsService;
        private readonly IUpdateAuthorService _updateAuthorService;
        private readonly IDeleteAuthorService _deleteAuthorService;

        public AuthorsController(
            ICreateAuthorService createAuthorService,
            IGetAuthorService getAuthorService,
            IGetAllAuthorsService getAllAuthorsService,
            IUpdateAuthorService updateAuthorService,
            IDeleteAuthorService deleteAuthorService)
        {
            _createAuthorService = createAuthorService;
            _getAuthorService = getAuthorService;
            _getAllAuthorsService = getAllAuthorsService;
            _updateAuthorService = updateAuthorService;
            _deleteAuthorService = deleteAuthorService;
        }

        // POST: api/authors
        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateAuthor([FromBody] CreateAuthorRequest createAuthorRequest)
        {
            var createdAuthor = await _createAuthorService.CreateAuthorAsync(createAuthorRequest);
            return Ok(new { message = "Author created successfully.", author = createdAuthor });
        }

        // GET: api/authors/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthorById(int id)
        {
            var author = await _getAuthorService.GetAuthorByIdAsync(id);
            if (author == null)
                return NotFound(new { error = "Author not found." });

            return Ok(author);
        }

        // GET: api/authors
        [HttpGet]
        public async Task<IActionResult> GetAllAuthors()
        {
            var authors = await _getAllAuthorsService.GetAllAuthorsAsync();
            return Ok(authors);
        }

        // PUT: api/authors/{id}
        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateAuthor(int id, [FromBody] UpdateAuthorRequest updateRequest)
        {
            var updatedAuthor = await _updateAuthorService.UpdateAuthorAsync(id, updateRequest);
            if (updatedAuthor == null)
                return NotFound(new { error = "Author not found or update failed." });

            return Ok(new { message = "Author updated successfully.", author = updatedAuthor });
        }

        // DELETE: api/authors/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var deleted = await _deleteAuthorService.DeleteAuthorAsync(id);
            if (!deleted)
                return NotFound(new { error = "Author not found or delete failed." });

            return Ok(new { message = "Author deleted successfully." });
        }
    }
}

