using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using BookManagement.Core.Entities;

namespace BookManagement.Infrastructure.Configurations
{
    public class GenreConfigurations : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.ToTable("Genres");

            builder.HasKey(g => g.GenreId);

            builder.Property(g => g.GenreName)
                .HasMaxLength(50)
                .IsRequired();

            builder.HasData(
                new Genre { GenreId = 1, GenreName = "Fiction" },
                new Genre { GenreId = 2, GenreName = "Non-Fiction" }
            );
        }
    }
}
