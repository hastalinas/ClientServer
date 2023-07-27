using API.Contracts;
using API.Data;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly BookingDBContext _dBContext;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository, BookingDBContext dbContext)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _dBContext = dbContext;
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
        if (!_employeeRepository.isNotExist(registerDto.Email) || !_employeeRepository.isNotExist(registerDto.PhoneNumber))
        {
            return 0;
        }

        var newNik = GenerateHandler.Nik(_employeeRepository.GetAutoNik());
        var employeeGuid = Guid.NewGuid();


        var employee = new Employee
        {
            Guid = employeeGuid,
            Nik = newNik,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            BirthDate = registerDto.BirthDate,
            Gender = registerDto.Gender,
            HiringDate = registerDto.HiringDate,
            Email = registerDto.Email,
            PhoneNumber = registerDto.PhoneNumber
        };
        _dBContext.Employees.Add(employee);


        var education = new Education
        {
            Guid = employeeGuid,
            Major = registerDto.Major,
            Degree = registerDto.Degree,
            GPA = (float)registerDto.GPA
        };
        _dBContext.Educations.Add(education);


        var existingUniversity = _universityRepository.GetByCode(registerDto.UniversityCode);
        if (existingUniversity is null)
        {

            var university = new University
            {
                Code = registerDto.UniversityCode,
                Name = registerDto.UniversitasName
            };
            _dBContext.Universities.Add(university);


            education.UniversityGuid = university.Guid;
        }
        else
        {

            education.UniversityGuid = existingUniversity.Guid;
        }


        var account = new Account
        {
            Guid = employeeGuid,
            //Otp = registerDto.OTP,
            Password = registerDto.Password
        };
        _dBContext.Accounts.Add(account);

        try
        {
            _dBContext.SaveChanges();
            return 1;
        }
        catch (Exception)
        {
            return -1;
        }
    }

    public int ForgotPasswordDto(ForgotPasswordDto forgotPasswordDto)
    {
        var employee = _employeeRepository.GetByEmail(forgotPasswordDto.Email);
        if (employee is null)
        {
            return 0; // Email not found
        }

        var account = _accountRepository.GetByGuid(employee.Guid);
        if (account is null)
        {
            return -1;
        }

        var otp = new Random().Next(111111, 999999);
        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = account.Guid,
            Password = account.Password,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            Otp = otp,
            IsUsed = false,
            CreatedDate = account.CreatedDate,
            ModifiedDate = DateTime.Now
        });

        if (!isUpdated)
        { 
            return -1; 
        }

        forgotPasswordDto.Email = $"{otp}";
        return 1;

    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var isExist = _employeeRepository.CheckEmail(changePasswordDto.Email);
        if (isExist is null)
        {
            return -1; //Account not found
        }

        var getAccount = _accountRepository.GetByGuid(isExist.Guid);
        var account = new Account
        {
            Guid = getAccount.Guid,
            IsUsed = true,
            ModifiedDate = DateTime.Now,
            CreatedDate = getAccount.CreatedDate,
            Otp = getAccount.Otp,
            ExpiredTime = getAccount.ExpiredTime,
            Password = changePasswordDto.NewPassword
        };
        if (getAccount.Otp != changePasswordDto.OTP)
        {
            return 0;
        }

        if (getAccount.IsUsed == true)
        {
            return 1;
        }

        if (getAccount.ExpiredTime < DateTime.Now)
        {
            return 2;
        }

        var isUpdated = _accountRepository.Update(account);
        if (!isUpdated)
        {
            return 0; //Account Not Update
        }
        return 3;
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
