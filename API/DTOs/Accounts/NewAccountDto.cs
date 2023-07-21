using API.Models;
namespace API.DTOs.Accounts;

public class NewAccountDto
{
    public Guid Guid { get; set; }
    public string Password { get; set; }
    public int OTP { get; set; }
    public bool IsUsed { get; set; }
    public DateTime ExpiredTime { get; set; }

    // post

    public static implicit operator Account(NewAccountDto newAccountDto)
    {
        return new Account
        {
            Guid = new Guid(),
            Otp = newAccountDto.OTP,
            Password = newAccountDto.Password,
            ExpiredTime = newAccountDto.ExpiredTime,
            IsUsed = newAccountDto.IsUsed,
            CreatedDate = DateTime.Now,
            ModifiedDate = DateTime.Now
        };
    }

    public static explicit operator NewAccountDto(Account account)
    {
        return new NewAccountDto
        {
            Guid = account.Guid,
            OTP = account.Otp,
            Password = account.Password,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
