using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.UserInterface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IAddUserService _addUserService;
        private readonly IGetAllUsersService _getAllUsersService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;
        private readonly IDeleteUserService _deleteUserService;

        public UsersController(
            IAddUserService addUserService,
            IGetAllUsersService getAllUsersService,
            IGetUserService getUserService,
            IUpdateUserService updateUserService,
            IDeleteUserService deleteUserService)
        {
            _addUserService = addUserService;
            _getAllUsersService = getAllUsersService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
            _deleteUserService = deleteUserService;
        }

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> GetAllUsers()
        {
            var users = await _getAllUsersService.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<UserResponse>> GetUserById(int id)
        {
            var user = await _getUserService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _addUserService.AddUserAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }


        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
        {
            var success = await _updateUserService.UpdateUserAsync(id, request);
            if (!success)
                return NotFound();
            return Ok(new { message = "User Updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            var success = await _deleteUserService.DeleteUserAsync(id);
            if (!success)
                return NotFound();
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
