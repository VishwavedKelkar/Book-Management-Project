
using System.ComponentModel.DataAnnotations;

namespace BookManagement.BL.DTOs.BookDTOs
{
    public class CreateBookRequest
    {
        [Required, StringLength(200)]
        public string Title { get; set; }

        [Required, StringLength(200)]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }

        [Required]
        public int AuthorId { get; set; }

        [Required]
        public int GenreId { get; set; }
        public int? CreatedBy { get; set; }

    }
}
