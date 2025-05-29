using BookManagement.Core.Entities;

namespace BookManagement.BL.Interfaces.Services.JwtTokenService
{
    public interface IJwtService
    {
        string GenerateToken(User user, IList<string> roles);
    }
}
