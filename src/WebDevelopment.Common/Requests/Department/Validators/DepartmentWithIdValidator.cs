
using FluentValidation;

namespace WebDevelopment.Common.Requests.Department.Validators
{
    public class DepartmentWithIdValidator : AbstractValidator<DepartmentWithIdRequest>
    {
        public DepartmentWithIdValidator()
        {
            RuleFor(dep => dep).SetValidator(new BaseDepartmentValidator());
            RuleFor(d => d.Id).NotNull().WithMessage("Please ensure you have entered your {PropertyName}");
        }
    }
}
