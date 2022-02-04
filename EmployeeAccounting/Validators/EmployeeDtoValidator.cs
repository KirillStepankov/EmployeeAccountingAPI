using EmployeeAccounting.DTO;
using FluentValidation;

namespace EmployeeAccounting.Validators
{
    public class EmployeeDtoValidator : AbstractValidator<EmployeeDto>
    {
        public EmployeeDtoValidator()
        {
            RuleFor(x => x.FIO).NotEmpty().NotNull().MaximumLength(250);
            RuleFor(x => x.DateAdded).NotEmpty().NotNull();
            RuleFor(x => x.DateModified).NotEmpty().NotNull();
            RuleFor(x => x.DateEmployment).NotEmpty().NotNull();
            RuleFor(x => x.Department).SetValidator(new DepartmentDtoValidator());
            RuleFor(x => x.Post).SetValidator(new PostDtoValidator());
        }
    }
}
