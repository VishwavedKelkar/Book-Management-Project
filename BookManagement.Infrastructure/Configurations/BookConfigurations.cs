
using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Configurations
{
    public class BookConfigurations : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(b => b.BookId);

            builder.Property(b => b.Title)
                .HasMaxLength(200)
                .IsRequired();

            builder.HasIndex(b => b.Title);

            builder.Property(b => b.ISBN)
                .HasMaxLength(20)
                .IsRequired();

            builder.HasIndex(b => b.ISBN)
                .IsUnique();

            builder.Property(b => b.PublishedDate)
                .IsRequired();

            // Foreign Key: Book -> Author
            builder.HasOne(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AuthorId);

            // Foreign Key: Book -> Genre
            builder.HasOne(b => b.Genres)
                .WithMany(g => g.Books)
                .HasForeignKey(b => b.GenreId);
        }
    }
}
