using FluentValidation;

namespace WebDevelopment.API.Model.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.Id).NotNull(); 
            RuleFor(user => user.Name).NotNull().Length(1, 20).WithMessage("Please ensure you have entered your {PropertyName}");
            RuleFor(user => user.Surname).NotNull().Length(1, 30).WithMessage("Please ensure you have entered your {PropertyName}");
            RuleFor(user => user.Email).EmailAddress().WithMessage("Please ensure you have entered your {PropertyName}");
        }
    }
}
