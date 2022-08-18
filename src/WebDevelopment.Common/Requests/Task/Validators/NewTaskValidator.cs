using FluentValidation;

namespace WebDevelopment.Common.Requests.Task.Validators;

public class NewTaskValidator : AbstractValidator<NewTaskRequest>
{
    public NewTaskValidator()
    {
        RuleFor(t => t).SetValidator(new BaseTaskValidator());
    }
}