﻿using API.Contracts;
using API.Models;
using API.DTOs.Employees;
using API.Utilities.Handlers;
using API.DTOs.Accounts;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public IEnumerable<EmployeeDto> GetAll()
    {
        var employees = _employeeRepository.GetAll();
        if (!employees.Any())
        {
            return Enumerable.Empty<EmployeeDto>(); // employee is null or not found;
        }

        var employeeDtos = new List<EmployeeDto>();
        foreach (var employee in employees)
        {
            employeeDtos.Add((EmployeeDto)employee);
        }

        return employeeDtos; // employee is found;
    }

    public EmployeeDto? GetByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return null; // employee is null or not found
        }
        return (EmployeeDto)employee;
    }

    public EmployeeDto? Create(NewEmployeeDto newEmployeeDto) 
    {
        Employee toCreate = newEmployeeDto;
        toCreate.Nik = GenerateHandler.Nik(_employeeRepository.GetAutoNik());

        var employee = _employeeRepository.Create(toCreate);
        if (employee is null)
        {
            return null; // employee is null or not found
        }
        return (EmployeeDto)employee;
    }

    public int Update(UpdateEmployeeNikDto employeeNikDto)
    {
        var employee = _employeeRepository.GetByGuid(employeeNikDto.Guid);
        //var existingEmployee = _employeeRepository.FirstOrDefault(e => e.Id == employee.Id);
        if (employee is null)
        {
            return -1; // employee is null or not found
        }

        Employee toUpdate = employeeNikDto;
        toUpdate.CreatedDate = employee.CreatedDate;
        toUpdate.Nik = _employeeRepository.GetByGuid(employeeNikDto.Guid).Nik;

        var result = _employeeRepository.Update(toUpdate);
        
        return result ? 1 // employee is updated
            : 0; // employee failed to update
    }

    public int Delete(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);
        if (employee is null)
        {
            return -1; // employee is null or not found
        }

        var result = _employeeRepository.Delete(employee);
        return result ? 1  // employee is deleted
            : 0; // employee failed to update
    }

    public OtpResponseDto? GetByEmail(string email)
    {
        var account = _employeeRepository.GetAll()
            .FirstOrDefault(e => e.Email.Contains(email));

        if (account != null)
        {
            return new OtpResponseDto
            {
                Email = account.Email,
                Guid = account.Guid
            };
        }

        return null;
    }
}
