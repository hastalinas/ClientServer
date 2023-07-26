using API.Contracts;
using API.DTOs.Accounts;
using API.Models;
using API.Repositories;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
    }

    public int Login(LoginDto loginDto)
    {
        var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);
        if (getEmployee is null)
        {
            return 0; // Employee not found
        }

        var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
        if (getAccount.Password == loginDto.Password)
        {
            return 1; // Login success
        }

        return 0;
    }

    public int Register(RegisterDto registerDto)
    {
        try
        {
            var account = new Account
            {
                Password = registerDto.Password
            };
            var employee = new Employee
            {
                Guid = new Guid(),
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                PhoneNumber = registerDto.PhoneNumber,
                BirthDate = registerDto.BirthDate,
                HiringDate = registerDto.HiringDate,
                Gender = registerDto.Gender,
            };
            var university = new University
            {
                Name = registerDto.UniversitasName
            };
            var education = new Education
            {
                Degree = registerDto.Degree,
                Major = registerDto.Major,
                GPA = registerDto.GPA
            };
            employee.Account = account;
            education.University = university;
            education.Employee = employee;

            var createemployee = _employeeRepository.Create(employee);
            var createuniversity = _universityRepository.Create(university);
            var createeducation = _educationRepository.Create(education);
            var createaccount = _accountRepository.Create(account);

            return 1;
        }
        catch
        {
            return 0;
        }

    }
    public IEnumerable<AccountDto> GetAll()
    {
        var accounts = _accountRepository.GetAll();
        if (!accounts.Any())
        {
            return Enumerable.Empty<AccountDto>(); // account is null or not found;
        }

        var accountDtos = new List<AccountDto>();
        foreach (var account in accounts)
        {
            accountDtos.Add((AccountDto)account);
        }

        return accountDtos; // account is found;
    }

    public AccountDto? GetByGuid(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        if (account is null)
        {
            return null; // account is null or not found;
        }

        return (AccountDto)account; // account is found;
    }

    public AccountDto? Create(NewAccountDto newAccountDto)
    {
        var account = _accountRepository.Create(newAccountDto);
        if (account is null)
        {
            return null; // account is null or not found;
        }

        return (AccountDto)account; // account is found;
    }

    public int Update(AccountDto accountDto)
    {
        var account = _accountRepository.GetByGuid(accountDto.Guid);
        if (account is null)
        {
            return -1; // account is null or not found;
        }

        Account toUpdate = accountDto;
        toUpdate.CreatedDate = account.CreatedDate;
        var result = _accountRepository.Update(toUpdate);

        return result ? 1 // account is updated;
            : 0; // account failed to update;
    }

    public int Delete(Guid guid)
    {
        var account = _accountRepository.GetByGuid(guid);
        //var getcreateemployee = _employeeRepository.Create();
        if (account is null)
        {
            return -1; // account is null or not found;
        }

        var result = _accountRepository.Delete(account);

        return result ? 1 // account is deleted;
            : 0; // account failed to delete;
    }
}
