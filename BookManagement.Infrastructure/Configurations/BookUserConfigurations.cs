using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Configurations
{
    public class BookUserConfigurations : IEntityTypeConfiguration<BookUser>
    {
        public void Configure(EntityTypeBuilder<BookUser> builder)
        {
            builder.ToTable("BookUsers");

            builder.HasKey(bu => new { bu.BookId, bu.UserId });

            builder.HasOne(bu => bu.Book)
                .WithMany(b => b.BookUsers)
                .HasForeignKey(bu => bu.BookId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bu => bu.User)
                .WithMany(u => u.BookUsers)
                .HasForeignKey(bu => bu.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
