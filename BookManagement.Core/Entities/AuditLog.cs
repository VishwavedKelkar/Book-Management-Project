using System.ComponentModel.DataAnnotations;

namespace BookManagement.Core.Entities
{
    public class AuditLog
    {
        public int AuditLogId { get; set; }

        [Required]
        public string EntityName { get; set; }        // can be Author,Book
        public int EntityId { get; set; }             //AuthorId, or BookId
        
        [Required]
        public string Action { get; set; }           // created,updated   
        public string? ChangedData { get; set; }        
        public DateTime Timestamp { get; set; } = DateTime.Now;

        public int? UserId { get; set; }                
        public virtual User? User { get; set; }

        public AuditLog()
        {
            Timestamp = DateTime.Now;
        }
    }
}
