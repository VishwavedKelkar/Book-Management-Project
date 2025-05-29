using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookManagement.DAL.Configurations
{
    public class AuditLogConfigurations : IEntityTypeConfiguration<AuditLog>
    {
        public void Configure(EntityTypeBuilder<AuditLog> builder)
        {
            builder.HasKey(a => a.AuditLogId);

            builder.Property(a => a.EntityName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(a => a.Action)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.HasOne(a => a.User)
                   .WithMany()
                   .HasForeignKey(a => a.UserId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.Property(a => a.Timestamp)
                    .HasDefaultValueSql("GETDATE()"); 

        }
    }
}
