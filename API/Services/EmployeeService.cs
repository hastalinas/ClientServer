using API.Contracts;
using API.Models;
using API.DTOs.Employees;
using API.Utilities.Handlers;
using API.DTOs.Accounts;

namespace API.Services;

public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEducationRepository _educationRepository;
    private readonly IUniversityRepository _universityRepository;
    public EmployeeService(IEmployeeRepository employeeRepository, IEducationRepository educationRepository, IUniversityRepository universityRepository)
    {
        _employeeRepository = employeeRepository;
        _educationRepository = educationRepository;
        _universityRepository = universityRepository;
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

    public IEnumerable<EmployeeDetailDto> GetAllEmployeeDetail()
    {
        var employees = _employeeRepository.GetAll();

        if (!employees.Any())
        {
            return Enumerable.Empty<EmployeeDetailDto>();
        }

        var employeesDetailDto = new List<EmployeeDetailDto>();

        foreach (var emp in employees)
        {
            var education = _educationRepository.GetByGuid(emp.Guid);
            var university = _universityRepository.GetByGuid(education.UniversityGuid);

            EmployeeDetailDto employeeDetail = new EmployeeDetailDto
            {
                EmployeeGuid = emp.Guid,
                NIK = emp.Nik,
                FullName = emp.FirstName + " " + emp.LastName,
                BirthDate = emp.BirthDate,
                Gender = emp.Gender,
                HiringDate = emp.HiringDate,
                Email = emp.Email,
                PhoneNumber = emp.PhoneNumber,
                Major = education.Major,
                Degree = education.Degree,
                GPA = education.GPA,
                UniversityName = university.Name
            };

            employeesDetailDto.Add(employeeDetail);
        };

        return employeesDetailDto; // employeeDetail is found;
    }

    public EmployeeDetailDto? GetEmployeeDetailByGuid(Guid guid)
    {
        var employee = _employeeRepository.GetByGuid(guid);

        if (employee is null)
        {
            return null;
        }
        var education = _educationRepository.GetByGuid(employee.Guid);
        var university = _universityRepository.GetByGuid(education.UniversityGuid);

        return new EmployeeDetailDto
        {
            EmployeeGuid = employee.Guid,
            NIK = employee.Nik,
            FullName = employee.FirstName + " " + employee.LastName,
            BirthDate = employee.BirthDate,
            Gender = employee.Gender,
            HiringDate = employee.HiringDate,
            Email = employee.Email,
            PhoneNumber = employee.PhoneNumber,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.GPA,
            UniversityName = university.Name
        }; ; // employeeDetail is found;
    }
}
