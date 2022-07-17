using System;
using System.Collections.Generic;

namespace WebDevelopment.Infrastructure.Models
{
    public partial class UsersSalary
    {
        public int Id { get; set; }
        public decimal? Salary { get; set; }
        public int? UserId { get; set; }
        public DateTimeOffset? ChangeTime { get; set; }

        public virtual User? User { get; set; }
    }
}
