namespace WebDevelopment.Infrastructure.Models;

public class Task
{
    public Task()
    {
        UserTasks = new HashSet<UserTask>();
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTimeOffset? CreationDate { get; set; }

    public virtual ICollection<UserTask> UserTasks { get; set; }

}