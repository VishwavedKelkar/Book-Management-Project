namespace BookManagement.BL.Interfaces.Services.BookInterface
{
    public interface IDeleteBookService
    {
        Task DeleteBookAsync(int bookId);
    }
}
