namespace WebDevelopment.Infrastructure.Models;

public class UserTask
{
    public int Id { get; set; }
    public int TaskId { get; set; }
    public int UserId { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? FinishDate { get; set; }

    public DateTimeOffset? ValidTill { get; set; } 

    public virtual User? User { get; set; }
    public virtual Task? Task { get; set; }

}