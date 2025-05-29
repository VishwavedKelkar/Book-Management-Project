namespace BookManagement.BL.DTOs.BookUserDTOs
{
    public class BookAssignmentResponse
    {
        public int UserId { get; set; }
        public string? Username { get; set; }
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
    }
}