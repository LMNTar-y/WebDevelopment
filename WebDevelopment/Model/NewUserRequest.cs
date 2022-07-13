namespace WebDevelopment.API.Model
{
    public class NewUserRequest : IUserRequest
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Email { get; set; }
    }
}
