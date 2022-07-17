using System;
using System.Collections.Generic;

namespace WebDevelopment.Infrastructure.Models
{
    public partial class Country
    {
        public Country()
        {
            SalaryRanges = new HashSet<SalaryRange>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Alpha3Code { get; set; }

        public virtual ICollection<SalaryRange> SalaryRanges { get; set; }
    }
}
