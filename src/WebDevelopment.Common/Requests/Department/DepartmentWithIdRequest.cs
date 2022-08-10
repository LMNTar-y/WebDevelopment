
namespace WebDevelopment.Common.Requests.Department
{
    public class DepartmentWithIdRequest : IDepartmentRequest
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
    }
}
