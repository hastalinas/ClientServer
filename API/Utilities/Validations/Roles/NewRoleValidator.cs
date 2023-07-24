using API.Contracts;
using API.DTOs.Roles;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Roles;

public class NewRoleValidator : AbstractValidator<NewRoleDto>
{
    private readonly IRoleRepository _roleRepository;
    public NewRoleValidator(IRoleRepository roleRepository)
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
