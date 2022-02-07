using EmployeeAccounting.DTO;
using EmployeeAccounting.Interfaces;
using FluentValidation;

namespace EmployeeAccounting.Validators
{
    public class PostDtoValidator : AbstractValidator<PostDto>
    {

        private readonly IPostRepository _repository;

        public PostDtoValidator()
        {
            _repository = repository;

            RuleFor(p => p.Name).NotEmpty().NotNull().Length(1,100);
            RuleFor(p => p).NotEmpty().NotNull();
        }
    }
}
