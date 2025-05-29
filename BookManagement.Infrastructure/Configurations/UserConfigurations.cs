using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.DAL.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.UserId);
            builder.Property(u => u.UserId).ValueGeneratedOnAdd();

            builder.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.HasIndex(u => u.Email).IsUnique();

            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(ur => ur.UserId)
                .OnDelete(DeleteBehavior.Cascade); 

            builder.HasMany(u => u.BookUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
