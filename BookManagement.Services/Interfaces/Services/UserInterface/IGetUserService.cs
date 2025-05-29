
using BookManagement.BL.DTOs.UserDTOs;

namespace BookManagement.BL.Interfaces.Services.UserInterface
{
    public interface IGetUserService
    {
        Task<UserResponse?> GetUserByIdAsync(int id);
    }
}
