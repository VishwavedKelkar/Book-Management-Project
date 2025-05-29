namespace BookManagement.BL.DTOs.AuthorDTOs
{
    public class UpdateAuthorRequest
    {
        public string AuthorName { get; set; }
        public string? Bio { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
