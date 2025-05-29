using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.DAL.Configurations
{
    public class BookImageConfigurations : IEntityTypeConfiguration<BookImage>
    {
        public void Configure(EntityTypeBuilder<BookImage> builder)
        {
            builder.ToTable("BookImages");

            builder.HasKey(bi => bi.BookImageId);
            builder.Property(bi => bi.BookImageId).ValueGeneratedOnAdd();

            builder.Property(bi => bi.ImageUrl)
                .IsRequired()
                .HasMaxLength(200); 

            builder.HasOne(bi => bi.Book)
                .WithMany(b => b.BookImages)
                .HasForeignKey(bi => bi.BookId)
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
