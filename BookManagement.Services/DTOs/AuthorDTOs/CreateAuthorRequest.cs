namespace BookManagement.Core.DTOs.AuthorDTOs
{
    public class CreateAuthorRequest
    {
        public string AuthorName { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
