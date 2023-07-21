using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class RoleRepository : GeneralRepository<Role>, IRoleRepository
{
    public RoleRepository(BookingDBContext context) : base(context) { }

    public IEnumerable<Role> GetByName(string name)
    {
        return _context.Set<Role>()
                       .Where(role => role.Name.Contains(name))
                       .ToList();
    }
}
