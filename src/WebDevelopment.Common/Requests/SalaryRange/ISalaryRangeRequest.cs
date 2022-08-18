﻿namespace WebDevelopment.Common.Requests.SalaryRange;

public interface ISalaryRangeRequest
{
    public decimal? StartRange { get; set; }
    public decimal? FinishRange { get; set; }
    public string? PositionName { get; set; }
    public string? CountryName { get; set; }
}