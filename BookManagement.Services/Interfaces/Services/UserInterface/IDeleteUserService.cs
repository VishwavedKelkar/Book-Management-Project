namespace BookManagement.BL.Interfaces.Services.UserInterface
{
    public interface IDeleteUserService
    {
        Task<bool> DeleteUserAsync(int id);
    }
}
