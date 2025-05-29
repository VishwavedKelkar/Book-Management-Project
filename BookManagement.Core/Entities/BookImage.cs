
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class BookImage
    {
        public int BookImageId { get; set; }

        [Required]

        public string ImageUrl { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
    }
}
