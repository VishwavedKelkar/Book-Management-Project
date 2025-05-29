using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public  class Genre
    {
        public int GenreId { get; set; }

        [Required]
        [StringLength(100)]
        public string GenreName { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
