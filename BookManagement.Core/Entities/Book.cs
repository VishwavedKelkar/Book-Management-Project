

using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class Book
    {
        public int BookId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; }

        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }

        public int GenreId { get; set; }
        public virtual Genre Genres { get; set; }


        public string? Description { get; set; } 
        public int? PageCount { get; set; }      

        public int? CreatedBy { get; set; }      
        public int? UpdatedBy { get; set; }      
        public virtual User? CreatedByUser { get; set; } 
        public virtual User? UpdatedByUser { get; set; } 

        public virtual ICollection<BookImage> BookImages { get; set; } = new List<BookImage>();
        public virtual ICollection<BookUser> BookUsers { get; set; } = new List<BookUser>();

        public Book()
        {
            CreatedDate = DateTime.UtcNow;
        }
    }
}
