using BookManagement.BL.DTOs.BookDTOs;
using BookManagement.BL.Interfaces.Services.BookInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ICreateBookService _createBookService;
        private readonly IGetAllBooksService _getAllBooksService;
        private readonly IGetBookService _getBookService;
        private readonly IUpdateBookService _updateBookService;
        private readonly IDeleteBookService _deleteBookService;

        public BookController(
            ICreateBookService createBookService,
            IGetAllBooksService getAllBooksService,
            IGetBookService getBookService,
            IUpdateBookService updateBookService,
            IDeleteBookService deleteBookService)
        {
            _createBookService = createBookService;
            _getAllBooksService = getAllBooksService;
            _getBookService = getBookService;
            _updateBookService = updateBookService;
            _deleteBookService = deleteBookService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook([FromBody] CreateBookRequest request)
        {
            var createdBook = await _createBookService.CreateBookAsync(request);

            if (createdBook == null)
                return BadRequest("Failed to create book");

            return Ok(createdBook); 
        }


        [HttpGet]
        public async Task<IActionResult> GetAllBooks()
        {
            var books = await _getAllBooksService.GetAllBooksAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetBookById(int id)
        {
            var book = await _getBookService.GetBookByIdAsync(id);
            if (book == null)
                return NotFound();

            return Ok(book);
        }

        [HttpPut("{bookId}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Update(int bookId, [FromForm] UpdateBookRequest request)
        {
            await _updateBookService.UpdateBookAsync(bookId, request);
            return NoContent();
        }


        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _deleteBookService.DeleteBookAsync(id);
            return Ok("Book deleted successfully.");
        }
    }
}
