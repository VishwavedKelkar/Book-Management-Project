
using System.ComponentModel.DataAnnotations;

namespace BookManagement.BL.DTOs.BookImageDTOs
{
    public class CreateBookImageRequest
    {
        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public int BookId { get; set; }
    }
}
