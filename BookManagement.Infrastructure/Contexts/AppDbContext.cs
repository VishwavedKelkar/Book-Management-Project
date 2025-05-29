using BookManagement.Core.Entities;
using BookManagement.DAL.Configurations;
using BookManagement.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Contexts
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<BookImage> BookImages { get; set; }
        public DbSet<BookUser> BookUsers { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AuthorConfigurations());
            modelBuilder.ApplyConfiguration(new BookConfigurations());
            modelBuilder.ApplyConfiguration(new BookImageConfigurations());
            modelBuilder.ApplyConfiguration(new BookUserConfigurations());
            modelBuilder.ApplyConfiguration(new GenreConfigurations());
            modelBuilder.ApplyConfiguration(new RoleConfigurations());
            modelBuilder.ApplyConfiguration(new UserConfigurations());
            modelBuilder.ApplyConfiguration(new UserRoleConfigurations());
            modelBuilder.ApplyConfiguration(new AuditLogConfigurations());
        }
    }
}
