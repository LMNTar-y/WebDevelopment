using FluentValidation;

namespace WebDevelopment.Common.Requests.LoginModel.Validators;

public class BaseUserLoginValidator : AbstractValidator<IUserLogin>
{
    public BaseUserLoginValidator()
    {
        RuleFor(u => u.UserName).NotEmpty().MinimumLength(5).WithMessage("Please ensure that you entered more than 5 letters");
        RuleFor(u => u.Password).NotEmpty().MinimumLength(8).MaximumLength(20).WithMessage("The password should contain more than 8 and less than 20 letters");
    }
}

public class UserLoginValidator : AbstractValidator<UserLogin>
{
    public UserLoginValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserLoginValidator());
    }
}

public class NewUserLoginValidator : AbstractValidator<NewUserLogin>
{
    public NewUserLoginValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserLoginValidator());
        RuleFor(u => u.UserEmail).EmailAddress().WithMessage("Please ensure that you entered an email");
    }
}