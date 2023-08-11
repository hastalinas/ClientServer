using API.DTOs.Employees;
using API.Models;
using Client.Contracts;
using Client.Repositories;

namespace Client.Repositories;

public class EmployeeRepository : GeneralRepository<Employee, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {
    }
}
