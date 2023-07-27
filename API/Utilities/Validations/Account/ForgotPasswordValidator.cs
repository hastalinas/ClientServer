using API.Contracts;
using FluentValidation;
using API.DTOs.Accounts;
using FluentValidation.AspNetCore;

namespace API.Utilities.Validations.Account;

public class ForgotPasswordValidator : AbstractValidator<ForgotPasswordDto>
{
    private readonly IEmployeeRepository _employeeRepository;
    public ForgotPasswordValidator(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;

        RuleFor(e => e.Email).NotEmpty().WithMessage("Email is required")
            .EmailAddress().WithMessage("Email is not valid");
    }
}
