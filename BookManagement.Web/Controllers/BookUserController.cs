using BookManagement.BL.DTOs.BookUserDTOs;
using BookManagement.BL.Interfaces.Services.BookUserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookUserController : ControllerBase
    {
        private readonly IAssignBookToUserService _assignService;
        private readonly IGetUserBooksService _getBooksService;
        private readonly IRemoveBookFromUserService _removeService;

        public BookUserController(
            IAssignBookToUserService assignService,
            IGetUserBooksService getBooksService,
            IRemoveBookFromUserService removeService)
        {
            _assignService = assignService;
            _getBooksService = getBooksService;
            _removeService = removeService;
        }

        [HttpPost("assign")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AssignBook([FromBody] AssignBookToUserRequest request)
        {
            var result = await _assignService.AssignAsync(request);
            if (result == null) return NotFound("User or Book not found.");
            return Ok(result);
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBooksForUser(int userId)
        {
            var books = await _getBooksService.GetBooksByUserIdAsync(userId);
            return Ok(books);
        }

        [HttpDelete("remove")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveBook([FromQuery] int userId, [FromQuery] int bookId)
        {
            var success = await _removeService.RemoveAsync(userId, bookId);
            if (!success) return NotFound("Assignment not found.");
            return Ok("Book removed from user.");
        }
    }
}
