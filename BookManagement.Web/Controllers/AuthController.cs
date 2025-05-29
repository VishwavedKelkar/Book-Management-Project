using BookManagement.BL.DTOs.UserDTOs;
using BookManagement.BL.Interfaces.Services.JwtTokenService;
using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using BookManagement.DAL.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly PasswordHasher<User> _passwordHasher = new PasswordHasher<User>();

    private readonly IJwtService _jwtService;

    private readonly AppDbContext _dbContext;

    public AuthController(IUserRepository userRepository, IJwtService jwtService, AppDbContext dbContext)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
        _dbContext = dbContext;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var existingUser = await _userRepository.GetByUsernameAsync(request.Username);
        if (existingUser != null)
            return BadRequest("Username already taken.");

        var user = new User
        {
            Username = request.Username,
            Email = request.Email
        };
        user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync(); // Save to generate UserId

        var userRoleEntity = await _dbContext.Roles.FirstOrDefaultAsync(r => r.RoleName == "User");
        var userRole = new UserRole { UserId = user.UserId, RoleId = userRoleEntity.RoleId };

        await _userRepository.AddUserRoleAsync(userRole);
        await _userRepository.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetByUsernameAsync(request.Username);
        if (user == null)
            return Unauthorized("Invalid username or password.");

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
        if (result == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid username or password.");

        var roles = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();

        var token = _jwtService.GenerateToken(user, roles);

        return Ok(new AuthResponse
        {
            Token = token,
            Username = user.Username,
            Email = user.Email
        });
    }
}
