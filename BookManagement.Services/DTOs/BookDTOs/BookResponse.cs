namespace BookManagement.BL.DTOs.BookDTOs
{
    public class BookResponse
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public string ISBN { get; set; }
        public DateTime PublishedDate { get; set; }
        public string? Description { get; set; }
        public int? PageCount { get; set; }

        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

        public int GenreId { get; set; }
        public string GenreName { get; set; }
    }
}
