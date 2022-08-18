using FluentValidation;

namespace WebDevelopment.Common.Requests.UserTask.Validators;

public class BaseUserTaskValidator : AbstractValidator<IUserTaskRequest>
{
    public BaseUserTaskValidator()
    {
        RuleFor(u => u.User).NotNull();
        RuleFor(u => u.Task).NotNull();
        RuleFor(u => u.FinishDate).GreaterThanOrEqualTo(u => u.StartDate);
        RuleFor(u => u.ValidTill).GreaterThanOrEqualTo(u => u.StartDate);
    }
}

public class NewUserTaskValidator : AbstractValidator<NewUserTaskRequest>
{
    public NewUserTaskValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserTaskValidator());
    }
}

public class UserTaskWithIdValidator : AbstractValidator<UserTaskWithIdRequest>
{
    public UserTaskWithIdValidator()
    {
        RuleFor(u => u).SetValidator(new BaseUserTaskValidator());
        RuleFor(u => u.Id).NotNull();
    }
}