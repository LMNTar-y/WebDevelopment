using FluentValidation;

namespace WebDevelopment.Common.Requests.Task.Validators;

public class TaskWithIdValidator : AbstractValidator<TaskWithIdRequest>
{
    public TaskWithIdValidator()
    {
        RuleFor(t => t).SetValidator(new BaseTaskValidator());
        RuleFor(t => t.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
    }
}