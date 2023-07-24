using API.Contracts;
using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRoleValidator;

public class AccountRoleValidator : AbstractValidator<AccountRoleDto>
{
    private readonly IAccounRoleRepository _accountRoleRepository;
    public AccountRoleValidator(IAccounRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
        RuleFor(ar => ar.RoleGuid).NotEmpty();
        RuleFor(ar => ar.AccountGuid).NotEmpty();
    }
}
