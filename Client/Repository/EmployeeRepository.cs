using API.DTOs.Employees;
using Client.Contracts;

namespace Client.Repository;

public class EmployeeRepository : GeneralRepository<NewEmployeeDto, Guid>, IEmployeeRepository
{
    public EmployeeRepository(string request = "employees/") : base(request)
    {
    }
}
