﻿using WebDevelopment.Common.Requests.SalaryRange;

namespace WebDevelopment.Domain.IRepos;

public interface ISalaryRangeRepo : IGenericRepository<ISalaryRangeRequest>
{
    Task<IEnumerable<ISalaryRangeRequest>> GetByNameAsync(string name);
}