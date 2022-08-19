
using FluentValidation;

namespace WebDevelopment.Common.Requests.Department.Validators
{
    public class BaseDepartmentValidator : AbstractValidator<IDepartmentRequest>
    {
        public BaseDepartmentValidator()
        {
            RuleFor(d => d.Name).NotEmpty().Length(5, 20).WithMessage("{PropertyName} should be more than 1 letter and less than 20");
        }
    }
}
