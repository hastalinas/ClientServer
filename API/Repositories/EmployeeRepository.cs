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

    public bool isNotExist(string value)
    {
        return _context.Set<Employee>().SingleOrDefault(e => e.PhoneNumber.Contains(value) || e.Email.Contains(value)) is null;
        //return _context.Set<Employee>().SingleOrDefault(e => e.Email == value || e.PhoneNumber == value) is null;
    }

    public string GetAutoNik()
    {
        return _context.Set<Employee>().ToList().LastOrDefault()?.Nik;
    }

    public Employee? GetByEmail(string email)
    {
        return _context.Set<Employee>().SingleOrDefault(e => e.Email.Contains(email));
    }

    public Guid GetLastEmployeeGuid()
    {
        return _context.Set<Employee>().ToList().LastOrDefault().Guid;
    }


    // implementasi dari IEmployeeRepository
    /*    public bool IsSameGuid(Guid guid)
        {
            Employee? employee = GetByGuid(guid);
            if (employee is null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }*/
}
