using BookManagement.BL.DTOs.GenreDTOs;
using BookManagement.BL.Interfaces.Services.GenreInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly ICreateGenreService _createGenreService;
        private readonly IGetAllGenresService _getAllGenresService;
        private readonly IGetGenreService _getGenreByIdService;
        private readonly IUpdateGenreService _updateGenreService;
        private readonly IDeleteGenreService _deleteGenreService;

        public GenreController(
            ICreateGenreService createGenreService,
            IGetAllGenresService getAllGenresService,
            IGetGenreService getGenreByIdService,
            IUpdateGenreService updateGenreService,
            IDeleteGenreService deleteGenreService)
        {
            _createGenreService = createGenreService;
            _getAllGenresService = getAllGenresService;
            _getGenreByIdService = getGenreByIdService;
            _updateGenreService = updateGenreService;
            _deleteGenreService = deleteGenreService;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> CreateGenre([FromBody] CreateGenreRequest request)
        {
            await _createGenreService.CreateGenreAsync(request);
            return Ok(new { message = "Genre created successfully." });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _getAllGenresService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _getGenreByIdService.GetGenreByIdAsync(id);
            if (genre is null) return NotFound();

            return Ok(genre);
        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> UpdateGenre(int id, [FromBody] UpdateGenreRequest request)
        {
            await _updateGenreService.UpdateGenreAsync(id, request);
            return Ok(new { message = "Genre updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            await _deleteGenreService.DeleteGenreAsync(id);
            return Ok(new { message = "Genre deleted successfully." });
        }
    }
}
