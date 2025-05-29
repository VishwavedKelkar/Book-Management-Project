using BookManagement.BL.DTOs.UserDTOs;

namespace BookManagement.BL.Interfaces.Services.UserInterface
{
    public interface IAddUserService
    {
        Task<UserResponse> AddUserAsync(CreateUserRequest request);
    }
}
