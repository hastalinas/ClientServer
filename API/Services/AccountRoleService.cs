using API.DTOs.AccountRoles;
using API.Models;
using API.Contracts;

namespace API.Services;

public class AccountRoleService
{
    private readonly IAccounRoleRepository _accountRoleRepository;

    public AccountRoleService(IAccounRoleRepository accountRoleRepository)
    {
        _accountRoleRepository = accountRoleRepository;
    }

    public IEnumerable<AccountRoleDto> GetAll()
    {
        var accountRoles = _accountRoleRepository.GetAll();
        if (!accountRoles.Any())
        {
            return Enumerable.Empty<AccountRoleDto>();
        }

        var accountRoleDtos = new List<AccountRoleDto>();
        foreach (var accountRole in accountRoles)
        {
            accountRoleDtos.Add((AccountRoleDto)accountRole);
        }

        return accountRoleDtos; // accountRole is found;
    }

    public AccountRoleDto? GetByGuid(Guid guid)
    {
        var accountRoleDto = _accountRoleRepository.GetByGuid(guid);
        if (accountRoleDto is null)
        {
            return null;
        }

        return (AccountRoleDto)accountRoleDto;
    }

    public AccountRoleDto? Create(NewAccountRoleDto newAccountRoleDto)
    {
        var accountRoleDto = _accountRoleRepository.Create(newAccountRoleDto);
        if (accountRoleDto is null)
        {
            return null;
        }

        return (AccountRoleDto)accountRoleDto;
    }

    public int Update(AccountRoleDto accountRoleDto)
    {
        var accountRole = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
        if (accountRole is null)
        {
            return -1; // accountRole is null or not found;
        }

        AccountRole toUpdate = accountRoleDto;
        toUpdate.CreatedDate = accountRole.CreatedDate;
        var result = _accountRoleRepository.Update(toUpdate);

        return result ? 1 // accountRole is updated;
            : 0; // accountRole failed to update;
    }

    public int Delete(Guid guid)
    {
        var accountRole = _accountRoleRepository.GetByGuid(guid);
        if (accountRole is null)
        {
            return -1; // accountRole is null or not found;
        }

        var result = _accountRoleRepository.Delete(accountRole);

        return result ? 1 // accountRole is deleted;
            : 0; // accountRole failed to delete;
    }
}
