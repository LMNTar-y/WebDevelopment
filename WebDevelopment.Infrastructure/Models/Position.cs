using System;
using System.Collections.Generic;

namespace WebDevelopment.Infrastructure.Models
{
    public partial class Position
    {
        public Position()
        {
            SalaryRanges = new HashSet<SalaryRange>();
            UserPositions = new HashSet<UserPosition>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ShortName { get; set; }

        public virtual ICollection<SalaryRange> SalaryRanges { get; set; }
        public virtual ICollection<UserPosition> UserPositions { get; set; }
    }
}
