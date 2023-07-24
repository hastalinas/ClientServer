using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class NewEmployeeDto
{
    //public string Nik { get; set; }
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public DateTime Birtdate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime Hiringdate { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
    {
        return new Employee
        {
            Guid = new Guid(),
            //Nik = newEmployeeDto.Nik,
            FirstName = newEmployeeDto.Firstname,
            LastName = newEmployeeDto.Lastname,
            BirthDate = newEmployeeDto.Birtdate,
            Gender = newEmployeeDto.Gender,
            Email = newEmployeeDto.Email,
            PhoneNumber = newEmployeeDto.Phone,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewEmployeeDto(Employee employee)
    {
        return new NewEmployeeDto
        {
            //Nik = employee.Nik,
            Firstname = employee.FirstName,
            Lastname = employee.LastName,
            Birtdate = employee.BirthDate,
            Gender = employee.Gender,
            Email = employee.Email,
            Phone = employee.PhoneNumber,
        };
    }
}
