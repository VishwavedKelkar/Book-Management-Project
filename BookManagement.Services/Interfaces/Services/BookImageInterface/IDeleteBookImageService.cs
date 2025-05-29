
namespace BookManagement.BL.Interfaces.Services.BookImageInterface
{
    public interface IDeleteBookImageService
    {
        Task<bool> DeleteBookImageAsync(int id);
    }
}
