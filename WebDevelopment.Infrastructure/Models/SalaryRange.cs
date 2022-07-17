using System;
using System.Collections.Generic;

namespace WebDevelopment.Infrastructure.Models
{
    public partial class SalaryRange
    {
        public int Id { get; set; }
        public decimal? StartRange { get; set; }
        public decimal? FinishRange { get; set; }
        public int? PositionId { get; set; }
        public int? CountryId { get; set; }
        public DateTimeOffset? CreationDate { get; set; }

        public virtual Country? Country { get; set; }
        public virtual Position? Position { get; set; }
    }
}
