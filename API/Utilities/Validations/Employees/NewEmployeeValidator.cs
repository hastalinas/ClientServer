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
        //RuleFor(e => e.Nik).NotEmpty().MaximumLength(5);

        RuleFor(e => e.Firstname).NotEmpty();

        RuleFor(e => e.Birtdate).NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10)).WithMessage("Age not availailable");

        RuleFor(e => e.Gender).NotNull()
            .IsInEnum();

        RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                                        .EmailAddress().WithMessage("Email is not valid")
                                        .Must(IsDuplicateValue).WithMessage("Email already exist");

        RuleFor(e => e.Hiringdate).NotEmpty().LessThanOrEqualTo(DateTime.Now.AddMonths(-3));

        RuleFor(e => e.Phone)
            .NotEmpty()
            .MaximumLength(20)
            .Matches("^(^\\+62|62|^08)(\\d{3,4}-?){2}\\d{3,4}$")
            .Must(IsDuplicateValue).WithMessage("Phone Number already exists");
    }

    private bool IsDuplicateValue(string arg)
    {
        return _employeeRepository.isNotExist(arg);
    }
}
