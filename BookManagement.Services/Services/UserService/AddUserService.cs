using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.Core.Entities;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.UserServices
{
    public class AddUserService : IAddUserService
    {
        private readonly IUserRepository _userRepository;

        public AddUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponse> AddUserAsync(CreateUserRequest request)
        {
            var user = new User
            {
                Username = request.Username,
                Email = request.Email
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            return new UserResponse
            {
                UserId = user.UserId,
                Username = user.Username,
                Email = user.Email
            };
        }
    }
}
