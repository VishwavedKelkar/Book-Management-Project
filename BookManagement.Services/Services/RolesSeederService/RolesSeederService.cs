
using BookManagement.BL.Interfaces.Services.SeedData;
using BookManagement.Core.Entities;
using BookManagement.DAL.Contexts;
using Microsoft.AspNetCore.Identity;

namespace BookManagement.Services.SeedData
{
    public class RolesSeederService : IDataSeeder
    {
        private readonly AppDbContext _context;

        public RolesSeederService(AppDbContext context)
        {
            _context = context;
        }

        public void Seed()
        {
            // Seed Roles if not exists
            if (!_context.Roles.Any())
            {
                _context.Roles.AddRange(
                    new Role { RoleName = "Admin" },
                    new Role { RoleName = "User" }
                );
                _context.SaveChanges();
            }

            // Seed default admin user if not exists
            if (!_context.Users.Any(u => u.Username == "admin"))
            {
                var passwordHasher = new PasswordHasher<User>();

                var admin = new User
                {
                    Username = "admin",
                    Email = "admin@gmail.com"
                };

                admin.PasswordHash = passwordHasher.HashPassword(admin, "AdminPassword@123"); 
                _context.Users.Add(admin);
                _context.SaveChanges();

                var adminRole = _context.Roles.First(r => r.RoleName == "Admin");

                _context.UserRoles.Add(new UserRole
                {
                    UserId = admin.UserId,
                    RoleId = adminRole.RoleId
                });

                _context.SaveChanges();
            }
        }
    }
}
