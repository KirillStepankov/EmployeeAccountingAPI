using EmployeeAccounting.DTO;
using FluentValidation;

namespace EmployeeAccounting.Validators
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {
        public PostDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().NotNull().Length(1,100);
            RuleFor(x => x).NotEmpty().NotNull();
        }
    }
}
