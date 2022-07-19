namespace WebDevelopment.Infrastructure.Models
{
    public partial class Department
    {
        public Department()
        {
            UserPositions = new HashSet<UserPosition>();
        }

        public Guid Id { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<UserPosition> UserPositions { get; set; }
    }
}
