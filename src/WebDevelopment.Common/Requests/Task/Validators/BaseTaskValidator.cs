using FluentValidation;

namespace WebDevelopment.Common.Requests.Task.Validators;

public class BaseTaskValidator : AbstractValidator<ITaskRequest>
{
    public BaseTaskValidator()
    {
        RuleFor(t => t.Name).NotEmpty().Length(3, 20).WithMessage("{PropertyName} should be more than 3 letter and less than 20");
        RuleFor(t => t.Description).NotEmpty().Length(20, 255).WithMessage("{PropertyName} should be more than 20 letter and less than 255");
    }
}