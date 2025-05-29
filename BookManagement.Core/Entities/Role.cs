
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();

    }
}
