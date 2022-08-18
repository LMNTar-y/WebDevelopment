namespace WebDevelopment.Common.Requests.User
{
    public class NewUserRequest : IUserRequest
    {
        public string? FirstName { get; set; }

        public string? SecondName { get; set; }

        public string? UserEmail { get; set; }
    }
}
