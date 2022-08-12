namespace WebDevelopment.Common.Requests.Task;

public interface ITaskRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}