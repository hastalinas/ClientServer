using API.DTOs.Accounts;
using FluentValidation;

namespace API.Utilities.Validations.Account;

public class LoginValidator : AbstractValidator<LoginDto>
{
    public LoginValidator()
    {
        RuleFor(ac => ac.Email).NotEmpty().EmailAddress();
        RuleFor(ac => ac.Password).NotEmpty();
    }
}
