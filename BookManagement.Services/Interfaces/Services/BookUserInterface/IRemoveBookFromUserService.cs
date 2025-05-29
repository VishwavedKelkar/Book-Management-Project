namespace BookManagement.BL.Interfaces.Services.BookUserInterface
{
    public interface IRemoveBookFromUserService
    {
        Task<bool> RemoveAsync(int userId, int bookId);
    }
}
