namespace WebDevelopment.Common.Requests.Task;

public class TaskWithIdRequest : ITaskRequest
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}