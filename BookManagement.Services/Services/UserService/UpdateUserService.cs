using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.UserServices
{
    public class UpdateUserService : IUpdateUserService
    {
        private readonly IUserRepository _userRepository;

        public UpdateUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            user.Username = request.Username;
            user.Email = request.Email;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
