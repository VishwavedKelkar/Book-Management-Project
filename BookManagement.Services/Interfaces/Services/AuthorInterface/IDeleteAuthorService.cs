namespace BookManagement.BL.Interfaces.Services.AuthorInterface
{
    public interface IDeleteAuthorService
    {
        Task<bool> DeleteAuthorAsync(int authorId);
    }
}
