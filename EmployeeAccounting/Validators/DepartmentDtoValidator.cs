using EmployeeAccounting.DTO;
using FluentValidation;

namespace EmployeeAccounting.Validators
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(1,100);
            RuleFor(x => x.DateAdded).NotEmpty().NotNull();
            RuleFor(x => x.DateModified).NotEmpty().NotNull();
        }
    }
}
