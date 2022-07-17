using System;
using System.Collections.Generic;

namespace WebDevelopment.Infrastructure.Models
{
    public partial class UserPosition
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? PositionId { get; set; }
        public Guid? DepartmentId { get; set; }
        public DateTimeOffset? StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Position? Position { get; set; }
        public virtual User? User { get; set; }
    }
}
