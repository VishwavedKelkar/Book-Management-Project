using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.BL.DTOs.BookImageDTOs
{
    public class BookImageUploadRequest
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public IFormFile Image { get; set; }
    }
}
