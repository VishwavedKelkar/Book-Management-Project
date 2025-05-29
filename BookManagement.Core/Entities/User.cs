using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class User
    {
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
        public virtual ICollection<BookUser> BookUsers { get; set; } = new List<BookUser>();
    }
}
