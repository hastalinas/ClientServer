﻿using API.Models;
using API.Utilities.Enums;

namespace API.DTOs.Employees;

public class NewEmployeeDto
{
    //public string Nik { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public GenderLevel Gender { get; set; }
    public DateTime HiringDate { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }

    public static implicit operator Employee(NewEmployeeDto newEmployeeDto)
    {
        return new Employee
        {
            Guid = new Guid(),
            //Nik = newEmployeeDto.Nik,
            FirstName = newEmployeeDto.FirstName,
            LastName = newEmployeeDto.LastName,
            BirthDate = newEmployeeDto.BirthDate,
            Gender = newEmployeeDto.Gender,
            Email = newEmployeeDto.Email,
            PhoneNumber = newEmployeeDto.PhoneNumber,
            HiringDate = newEmployeeDto.HiringDate,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewEmployeeDto(Employee employee)
    {
        return new NewEmployeeDto
        {
            //Nik = employee.Nik,
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
