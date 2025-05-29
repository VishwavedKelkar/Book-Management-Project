
using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Configurations
{
    public class RoleConfigurations : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.RoleId);
            builder.Property(r => r.RoleId).ValueGeneratedOnAdd();

            builder.Property(r => r.RoleName)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasMany(r => r.UserRoles)
                .WithOne(ur => ur.Role)
                .HasForeignKey(ur => ur.RoleId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
