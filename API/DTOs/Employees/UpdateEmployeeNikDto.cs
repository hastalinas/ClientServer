using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class UpdateEmployeeNikDto
{
    public Guid Guid { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime Birtdate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime Hiringdate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public static implicit operator Employee(UpdateEmployeeNikDto updateEmployeeDto)
    {
        return new Employee
        {
            Guid = updateEmployeeDto.Guid,
            FirstName = updateEmployeeDto.Firstname,
            LastName = updateEmployeeDto.Lastname,
            BirthDate = updateEmployeeDto.Birtdate,
            Gender = updateEmployeeDto.Gender,
            HiringDate = updateEmployeeDto.Hiringdate,
            Email = updateEmployeeDto.Email,
            PhoneNumber = updateEmployeeDto.Phone,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator UpdateEmployeeNikDto(Employee employee)
    {
        return new UpdateEmployeeNikDto
        {
            Guid = employee.Guid,
            Firstname = employee.FirstName,
            Lastname = employee.LastName,
            Birtdate = employee.BirthDate,
            Hiringdate = employee.HiringDate,
            Gender = employee.Gender,
            Email = employee.Email,
            Phone = employee.PhoneNumber,
        };
    }
}
