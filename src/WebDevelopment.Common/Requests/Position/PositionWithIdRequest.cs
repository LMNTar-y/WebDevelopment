namespace WebDevelopment.Common.Requests.Position;

public class PositionWithIdRequest : IPositionRequest
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? ShortName { get; set; }
}