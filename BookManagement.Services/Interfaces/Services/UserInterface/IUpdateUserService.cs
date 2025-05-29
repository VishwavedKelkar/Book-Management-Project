
using BookManagement.BL.DTOs.UserDTOs;

namespace BookManagement.BL.Interfaces.Services.UserInterface
{
    public interface IUpdateUserService
    {
        Task<bool> UpdateUserAsync(int id, UpdateUserRequest request);
    }
}
