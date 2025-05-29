    
namespace BookManagement.Core.Entities
{
    public class BookUser
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
