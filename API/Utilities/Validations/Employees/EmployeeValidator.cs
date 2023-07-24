using API.Contracts;
using FluentValidation;
using API.DTOs.Employees;

namespace API.Utilities.Validations.Employees;

public class EmployeeValidator : AbstractValidator<EmployeeDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        RuleFor(e => e.Nik).NotEmpty().MaximumLength(6).WithMessage("Max charcter 6");

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

    private bool IsDuplicateValue(string value)
    {
        return _employeeRepository.isNotExist(value);
    }
}

