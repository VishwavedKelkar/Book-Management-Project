﻿namespace BookManagement.Core.Entities
{
    public class UserRole
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
