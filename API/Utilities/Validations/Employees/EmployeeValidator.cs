using API.Contracts;
using FluentValidation;
using API.DTOs.Employees;
using API.Models;

namespace API.Utilities.Validations.Employees;

public class EmployeeValidator : AbstractValidator<UpdateEmployeeNikDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
        //RuleFor(e => e.Nik).NotEmpty().MaximumLength(5).WithMessage("Max charcter 5");

        RuleFor(e => e.FirstName).NotEmpty();

        RuleFor(e => e.BirthDate).NotEmpty()
            .LessThanOrEqualTo(DateTime.Now.AddYears(-10));

        RuleFor(e => e.Gender).NotNull()
            .IsInEnum();

        RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
                                        .EmailAddress().WithMessage("Email is not valid")
                                        //.Must((e, email) => Check(employeeRepository.GetByGuid, email) is true).WithMessage("Email already exist")
                                        .Must(IsDuplicateValue).WithMessage("Email already exists"); //cek email

        RuleFor(e => e.HiringDate).NotEmpty();

        RuleFor(e => e.PhoneNumber)
            .NotEmpty()
            .MaximumLength(20)
            .Matches("^(^\\+62|62|^08)(\\d{3,4}-?){2}\\d{3,4}$")
            //.Must((e, phone) => Check(employeeRepository.GetByGuid, phone) is true).WithMessage("Phone number is already exists")
            .Must(IsDuplicateValue).WithMessage("Phone number is already exists"); 
    }

    private bool IsDuplicateValue(string value)
    {
        return _employeeRepository.isNotExist(value);
    }


}

