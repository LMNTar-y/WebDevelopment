namespace WebDevelopment.Common.Requests.Position;

public class NewPositionRequest : IPositionRequest
{
    public string? Name { get; set; }

    public string? ShortName { get; set; }
}