namespace WebDevelopment.Common.Requests.SalaryRange;

public class SalaryRangeWithIdRequest : ISalaryRangeRequest
{
    public int Id { get; set; }
    public decimal? StartRange { get; set; }
    public decimal? FinishRange { get; set; }
    public string? PositionName { get; set; }
    public string? CountryName { get; set; }
}