using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EmployeeRepository : GeneralRepository<Employee>, IEmployeeRepository
{
    public EmployeeRepository(BookingDBContext context) : base(context) { }

    public IEnumerable<Employee> GetByName(string name)
    {
        return _context.Set<Employee>()
                       .Where(employee => employee.FirstName.Contains(name))
                       .ToList();
    }
}
