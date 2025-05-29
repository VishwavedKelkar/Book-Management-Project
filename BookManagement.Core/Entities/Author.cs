
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class Author
    {
        public int AuthorId { get; set; }

        [Required]
        public string AuthorName { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
