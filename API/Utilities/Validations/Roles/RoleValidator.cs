using API.Contracts;
using API.DTOs.Roles;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class RoleValidator : AbstractValidator<RoleDto>
{
    private readonly IRoleRepository _roleRepository;
    public RoleValidator(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
        RuleFor(role => role.Name).NotEmpty().WithMessage("Name is required")
                                  .Must(IsDuplicateValue).WithMessage("Name is duplicated");
    }
    private bool IsDuplicateValue(string arg)
    {
        return _roleRepository.isNotExist(arg);
    }
}
