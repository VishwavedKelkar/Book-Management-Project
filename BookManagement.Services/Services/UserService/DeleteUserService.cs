using BookManagement.BL.Interfaces.Services.UserInterface;
using BookManagement.DAL.Interfaces.Repositories;

namespace BookManagement.BL.Services.UserServices
{
    public class DeleteUserService : IDeleteUserService
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepository.DeleteAsync(id);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
