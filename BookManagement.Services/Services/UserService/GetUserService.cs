using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.UserServices
{
    public class GetUserService : IGetUserService
    {
        private readonly IUserRepository _userRepository;

        public GetUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
