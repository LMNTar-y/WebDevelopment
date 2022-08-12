namespace WebDevelopment.Common.Requests.Task;

public class NewTaskRequest : ITaskRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}