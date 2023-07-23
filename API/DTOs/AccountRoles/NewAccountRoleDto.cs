using API.Models;

namespace API.DTOs.AccountRoles;

public class NewAccountRoleDto
{
    public Guid AccountGuid { get; set; }
    public Guid RoleGuid { get; set; }

    public static implicit operator AccountRole(NewAccountRoleDto newAccountRoleDto)
    {
        return new AccountRole
        {
            Guid = Guid.NewGuid(),
            AccountGuid = newAccountRoleDto.AccountGuid,
            RoleGuid = newAccountRoleDto.RoleGuid,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewAccountRoleDto(AccountRole AccountRole)
    {
        return new NewAccountRoleDto
        {
            AccountGuid = AccountRole.AccountGuid,
            RoleGuid = AccountRole.RoleGuid
        };
    }
}
