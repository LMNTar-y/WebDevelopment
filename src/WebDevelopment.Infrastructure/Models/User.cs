namespace WebDevelopment.Infrastructure.Models
{
    public partial class User
    {
        public User()
        {
            UserPositions = new HashSet<UserPosition>();
            UsersSalaries = new HashSet<UsersSalary>();
            UserTasks = new HashSet<UserTask>();
        }

        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }
        public string? UserEmail { get; set; }
        public bool? Active { get; set; }

        public virtual ICollection<UserPosition> UserPositions { get; set; }
        public virtual ICollection<UsersSalary> UsersSalaries { get; set; }
        public virtual ICollection<UserTask> UserTasks { get; set; }
    }
}
