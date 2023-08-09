using API.DTOs.Employees;
using API.Models;

namespace Client.Contracts;

public interface IEmployeeRepository : IRepository<NewEmployeeDto, Guid>
{
}
