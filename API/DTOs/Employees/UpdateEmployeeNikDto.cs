using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class UpdateEmployeeNikDto
{
    public Guid Guid { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static implicit operator Employee(UpdateEmployeeNikDto updateEmployeeDto)
    {
        return new Employee
        {
            Guid = updateEmployeeDto.Guid,
            FirstName = updateEmployeeDto.FirstName,
            LastName = updateEmployeeDto.LastName,
            BirthDate = updateEmployeeDto.BirthDate,
            Gender = updateEmployeeDto.Gender,
            HiringDate = updateEmployeeDto.HiringDate,
            Email = updateEmployeeDto.Email,
            PhoneNumber = updateEmployeeDto.PhoneNumber,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator UpdateEmployeeNikDto(Employee employee)
    {
        return new UpdateEmployeeNikDto
        {
            Guid = employee.Guid,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            BirthDate = employee.BirthDate,
            HiringDate = employee.HiringDate,
            Gender = employee.Gender,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
        };
    }
}
