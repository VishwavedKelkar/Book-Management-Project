
using System.ComponentModel.DataAnnotations;

namespace BookManagement.BL.DTOs.UserDTOs
{
    public class UpdateUserRequest
    {
        [Required, StringLength(50)]
        public string Username { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }
    }
}
