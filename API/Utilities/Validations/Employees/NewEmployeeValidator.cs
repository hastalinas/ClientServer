using API.Contracts;
using API.DTOs.Employees;
using FluentValidation;

namespace API.Utilities.Validations.Employees;

public class NewEmployeeValidator : AbstractValidator<NewEmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;

    public NewEmployeeValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        RuleFor(e => e.Nik).NotEmpty().MaximumLength(6);

        RuleFor(e => e.Firstname).NotEmpty();

        RuleFor(e => e.Birtdate).NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

        RuleFor(e => e.Gender).NotNull()
            .IsInEnum();

        RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                                        .EmailAddress().WithMessage("Email is not valid")
                                        .Must(IsDuplicateValue).WithMessage("Email already exist");

        RuleFor(e => e.Hiringdate).NotEmpty();

        RuleFor(e => e.Phone).NotEmpty()
            .MaximumLength(13).Matches(@"^\+[0-9]");
    }

    private bool IsDuplicateValue(string arg)
    {
        return _employeeRepository.isNotExist(arg);
    }
}
