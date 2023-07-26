using API.Utilities.Enums;
using Microsoft.AspNetCore.Identity;

namespace API.DTOs.Accounts;
public class RegisterDto
{
    //employee
    public string FirstName { get; set; }
    public string? LastName { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }
    public DateTime HiringDate { get; set; }
    public GenderLevel Gender { get; set; }
    public string PhoneNumber { get; set; }

    // education
    public string Major { get; set; }
    public float GPA { get; set; }
    public string Degree { get; set; }

    // university
    public string UniversitasName { get; set; }
    public string UniversityCode { get; set; }

    // account
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public int OTP { get; set; }
}
