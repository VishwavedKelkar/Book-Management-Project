namespace BookManagement.Core.DTOs.AuthorDTOs
{
    public class AuthorResponse
    {
        public int AuthorId { get; set; }
        public string Name { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
