using BookManagement.BL.DTOs.UserDTOs;

namespace BookManagement.BL.Interfaces.Services.UserInterface
{
    public interface IGetAllUsersService
    {
        Task<IEnumerable<UserResponse>> GetAllUsersAsync();
    }
}
