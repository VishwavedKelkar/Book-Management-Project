
using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.Infrastructure.Configurations
{
        public class AuthorConfigurations : IEntityTypeConfiguration<Author>
        {
            public void Configure(EntityTypeBuilder<Author> builder)
            {
                builder.ToTable("Authors");

                builder.HasKey(a => a.AuthorId);

                builder.Property(a => a.AuthorName)
                    .HasMaxLength(100)
                    .IsRequired();

                builder.Property(a => a.Bio)
                    .HasMaxLength(500)
                    .IsRequired(false);

                builder.Property(a => a.DateOfBirth)
                    .IsRequired();

                // One to Many: Author -> Books
                builder.HasMany(a => a.Books)
                    .WithOne(b => b.Author)
                    .HasForeignKey(b => b.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade); 
            }
        }
}
