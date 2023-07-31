
using API.Models;
using System.Net.NetworkInformation;

namespace API.DTOs.Roles;

public class NewRoleDafaultDto
{
    public Guid Guid { get; set; }
    public string Name { get; set; }

    public static implicit operator Role(NewRoleDafaultDto newRoleDafaultDto)
    {
        return new Role
        {
            Guid = newRoleDafaultDto.Guid,
            Name = newRoleDafaultDto.Name,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewRoleDafaultDto(Role role)
    {
        return new NewRoleDafaultDto
        {
            Name = role.Name
        };
    }
}
