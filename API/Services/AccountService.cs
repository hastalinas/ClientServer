using API.Contracts;
using API.Data;
using API.DTOs.AccountRoles;
using API.DTOs.Accounts;
using API.DTOs.Educations;
using API.DTOs.Employees;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Utilities.Handlers;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TokenHandler = Microsoft.IdentityModel.Tokens.TokenHandler;

namespace API.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    private readonly IAccounRoleRepository _accountRoleRepository;
    private readonly IEmailHandler _emailHandler;
    private readonly ITokenHandler _tokenHandler;
    private readonly BookingDBContext _dBContext;

    public AccountService(
        IAccountRepository accountRepository, 
        IEmployeeRepository employeeRepository, 
        IUniversityRepository universityRepository, 
        IEducationRepository educationRepository, 
        IAccounRoleRepository accounRoleRepository,
        IEmailHandler emailHandler, 
        ITokenHandler tokenHandler,
        BookingDBContext dbContext)
    {
        _accountRepository = accountRepository;
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
        _accountRoleRepository = accounRoleRepository;
        _emailHandler = emailHandler;
        _tokenHandler = tokenHandler;
        _dBContext = dbContext;
    }

    public string Login(LoginDto loginDto)
    {
        /*
         */
        try
        {
            var getEmployee = _employeeRepository.GetByEmail(loginDto.Email);
            if (getEmployee is null)
            {
                return "-1"; // Employee not found
            }

            var getAccount = _accountRepository.GetByGuid(getEmployee.Guid);
            if (!HashingHandler.ValidateHash(loginDto.Password, getAccount.Password))
            {
                return "-1";
            }

            var employee = _employeeRepository.GetByEmail(loginDto.Email);
            var getRoles = _accountRoleRepository.GetRoleNamesByAccountGuid(employee.Guid);

            var claims = new List<Claim>
            {
                new Claim("Guid", getEmployee.Guid.ToString()),
                new Claim("FullName", $"{getEmployee.FirstName } {getEmployee.LastName }"),
                new Claim("Email", getEmployee.Email)
            };

            foreach (var role in getRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var generateToken = _tokenHandler.GenerateToken(claims);
            if (generateToken is null)
            {
                return "-2";
            }
            return generateToken; // Login success
        }
        catch
        {
            return "-2";
        }

    }

    public int Register(RegisterDto registerDto)
    {
        if (!_employeeRepository.isNotExist(registerDto.Email) || 
            !_employeeRepository.isNotExist(registerDto.PhoneNumber))
        {
            return 0;
        }

        using var transaction = _dBContext.Database.BeginTransaction();
        try
        {
            var university = _universityRepository.GetByCode(registerDto.UniversityCode);
            if (university is null)
            {
                // Jika universitas belum ada, buat objek University baru dan simpan
                var createUniversity = _universityRepository.Create(new University
                {
                    Code = registerDto.UniversityCode,
                    Name = registerDto.UniversitasName
                });

                university = createUniversity;
            }

            var newNik =
                GenerateHandler.Nik(_employeeRepository.GetAutoNik()); //karena niknya generate
            var employeeGuid = Guid.NewGuid(); // Generate GUID baru untuk employee

            // Buat objek Employee dengan nilai GUID baru
            var employee = _employeeRepository.Create(new Employee
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
            });


            var education = _educationRepository.Create(new Education
            {
                Guid = employeeGuid, 
                Major = registerDto.Major,
                Degree = registerDto.Degree,
                GPA = registerDto.GPA,
                UniversityGuid = university.Guid
            });

            var account = _accountRepository.Create(new Account
            {
                Guid = employeeGuid, 
                Otp = 1,            
                IsUsed = true,
                Password = HashingHandler.GenerateHash(registerDto.Password) // hashing password
            });

            var accountRole = _accountRoleRepository.Create(new NewAccountRoleDto
            {
                AccountGuid = account.Guid,
                RoleGuid = Guid.Parse("4887ec13-b482-47b3-9b24-08db91a71770")
            });

            transaction.Commit();
            return 1;
        }
        catch
        {
            transaction.Rollback();
            return -1;
        }
    }

    public int ForgotPasswordDto(ForgotPasswordDto forgotPasswordDto)
    {
        var otp = new Random().Next(111111, 999999);
        var getAccountDetail = (from e in _employeeRepository.GetAll()
                                join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                                where e.Email == forgotPasswordDto.Email
                                select a).FirstOrDefault();

        if (getAccountDetail is null)
        {
            return 0; // no email found
        }

        _accountRepository.Clear();

        var isUpdated = _accountRepository.Update(new Account
        {
            Guid = getAccountDetail.Guid,
            Password = getAccountDetail.Password,
            ExpiredTime = DateTime.Now.AddMinutes(5),
            Otp = otp,
            IsUsed = false,
            CreatedDate = getAccountDetail.CreatedDate,
            ModifiedDate = getAccountDetail.ModifiedDate
        });

        if (!isUpdated)
        {
            return -1; // error update
        }

        _emailHandler.SendEmail(forgotPasswordDto.Email, "OTP", $"Your OTP id {otp}");

        return 1;
    }

    public int ChangePassword(ChangePasswordDto changePasswordDto)
    {
        var getAccount = (from e in _employeeRepository.GetAll()
                          join a in _accountRepository.GetAll() on e.Guid equals a.Guid
                          where e.Email == changePasswordDto.Email
                          select a).FirstOrDefault();

        if (getAccount is null)
        {
            return 0;
        }
        var hashedPassword = HashingHandler.GenerateHash(changePasswordDto.NewPassword);
        var account = new Account
        {
            Guid = getAccount.Guid,
            IsUsed = true,
            ModifiedDate = DateTime.Now,
            CreatedDate = getAccount.CreatedDate,
            Otp = getAccount.Otp,
            ExpiredTime = getAccount.ExpiredTime,
            Password = hashedPassword,
        };

        if (getAccount.Otp != changePasswordDto.OTP)
        {
            return -1;
        }

        if (getAccount.IsUsed == true)
        {
            return -2;
        }

        if (getAccount.ExpiredTime < DateTime.Now)
        {
            return -3; // OTP expired
        }

        _accountRepository.Clear();

        var isUpdated = _accountRepository.Update(account);
        if (!isUpdated)
        {
            return -4; //Account not Update
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
