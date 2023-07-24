using API.Contracts;
using API.DTOs.AccountRoles;
using FluentValidation;

namespace API.Utilities.Validations.AccountRole;

public class NewAccountRoleValidator : AbstractValidator<NewAccountRoleDto>
{
    private readonly IAccounRoleRepository _accountRoleRepository;
    public NewAccountRoleValidator(IAccounRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
        RuleFor(ar => ar.RoleGuid).NotEmpty();
        RuleFor(ar => ar.AccountGuid).NotEmpty();
    }
}
