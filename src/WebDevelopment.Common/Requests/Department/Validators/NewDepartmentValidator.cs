
using FluentValidation;

namespace WebDevelopment.Common.Requests.Department.Validators
{
    public class NewDepartmentValidator : AbstractValidator<NewDepartmentRequest>
    {
        public NewDepartmentValidator()
        {
            RuleFor(dep => dep).SetValidator(new BaseDepartmentValidator());
        }
    }
}
