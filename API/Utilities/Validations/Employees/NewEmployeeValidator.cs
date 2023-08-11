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

        RuleFor(e => e.FirstName).NotEmpty();

        RuleFor(e => e.BirthDate).NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10)).WithMessage("Age not availailable");

        RuleFor(e => e.Gender).NotNull()
            .IsInEnum();

        RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                                        .EmailAddress().WithMessage("Email is not valid")
                                        .Must(IsDuplicateValue).WithMessage("Email already exist");


        RuleFor(e => e.HiringDate).NotEmpty();

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
            .MaximumLength(15)
            .Matches("^(^\\+62|62|^08)(\\d{3,4}-?){2}\\d{3,4}$")
            .Must(IsDuplicateValue).WithMessage("Phone number is already exists");
    }

    private bool IsDuplicateValue(string value)
    {
        return _employeeRepository.isNotExist(value);
    }
}
