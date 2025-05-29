using BookManagement.BL.DTOs.BookImageDTOs;
using BookManagement.BL.Interfaces.Services.BookImageInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;

namespace BookManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookImageController : ControllerBase
    {
        private readonly IAddBookImageService _addService;
        private readonly IGetAllBookImagesService _getAllService;
        private readonly IGetBookImageService _getByIdService;
        private readonly IUpdateBookImageService _updateService;
        private readonly IDeleteBookImageService _deleteService;

        private readonly IWebHostEnvironment _hostingEnvironment;

        public BookImageController(
            IAddBookImageService addService,
            IGetAllBookImagesService getAllService,
            IGetBookImageService getByIdService,
            IUpdateBookImageService updateService,
            IDeleteBookImageService deleteService,
            IWebHostEnvironment hostingEnvironment)  
        {
            _addService = addService;
            _getAllService = getAllService;
            _getByIdService = getByIdService;
            _updateService = updateService;
            _deleteService = deleteService;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpPost("upload")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> Create([FromForm] BookImageUploadRequest request)

        {
            if (request.Image == null || request.Image.Length == 0)
                return BadRequest("No image file provided.");

            var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(request.Image.FileName);
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await request.Image.CopyToAsync(fileStream);
            }

            var imageUrl = "/uploads/" + uniqueFileName;

            var createRequest = new CreateBookImageRequest
            {
                ImageUrl = imageUrl,
                BookId = request.BookId
            };

            var result = await _addService.AddBookImageAsync(createRequest);

            return CreatedAtAction(nameof(GetById), new { id = result.BookImageId }, result);

        }


        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _getAllService.GetAllBookImagesAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _getByIdService.GetBookImageByIdAsync(id);
            if (result == null) return NotFound();

            return Ok(result);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateBookImageRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(request.ImageUrl))
                return BadRequest("ImageUrl cannot be null or empty.");

            try
            {
                var result = await _updateService.UpdateBookImageAsync(id, request);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the book image.");
            }
        }



        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _deleteService.DeleteBookImageAsync(id);
            if (!deleted) return NotFound();

            return NoContent();
        }
    }
}
