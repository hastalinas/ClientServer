using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;

    public AccountService(IAccountRepository accountRepository, IEmployeeRepository employeeRepository, IUniversityRepository universityRepository, IEducationRepository educationRepository)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;

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

    public RegisterDto? Register(RegisterDto registerDto)
    {
        Employee createEmployee = new NewEmployeeDto
        {
            Firstname = registerDto.FirstName,
            Lastname = registerDto.LastName,
            Birtdate = registerDto.BirthDate,
            Gender = registerDto.Gender,
            Hiringdate = registerDto.HiringDate,
            Email = registerDto.Email,
            Phone = registerDto.PhoneNumber
        };

        createEmployee.Nik = GenerateHandler.Nik(_employeeRepository.GetAutoNik());
        var employeeResult = _employeeRepository.Create(createEmployee);
        
        var universityResult = _universityRepository.Create(new NewUniversityDto
        {
            Code = registerDto.UniversityCode,
            Name = registerDto.UniversityCode
        });

        var educationResullt = _educationRepository.Create(new NewEducationDto
        {
            Guid = _employeeRepository.GetLastEmployeeGuid(),
            Degree = registerDto.Degree,
            Major = registerDto.Major,
            GPA = registerDto.GPA,
            UniversityGuid = _universityRepository.GetLastUniversityGuid()
        });

        var accountResult = _accountRepository.Create(new NewAccountDto
        {
            Guid = _employeeRepository.GetLastEmployeeGuid(),
            IsUsed = true,
            ExpiredTime = DateTime.Now.AddYears(3),
            OTP = registerDto.OTP,
            Password = registerDto.Password,
        });

        if (employeeResult is null || universityResult is null || educationResullt is null || accountResult is null)
        {
            return null;
        }

        return (RegisterDto)registerDto;

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
