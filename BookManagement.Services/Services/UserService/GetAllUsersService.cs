using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.UserServices
{
    public class GetAllUsersService : IGetAllUsersService
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserResponse>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponse
            {
                UserId = u.UserId,
                Username = u.Username,
                Email = u.Email
            });
        }
    }
}
