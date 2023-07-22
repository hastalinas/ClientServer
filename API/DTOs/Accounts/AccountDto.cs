using API.Models;

namespace API.DTOs.Accounts;

public class AccountDto
{
    public Guid Guid { get; set; }
    public int Otp { get; set; }
    public DateTime ExpiredTime { get; set; }
    public bool IsUsed { get; set; }

    public static implicit operator Account(AccountDto accountDto)
    {
        return new Account
        {
            Guid = accountDto.Guid,
            Otp = accountDto.Otp,
            IsUsed = accountDto.IsUsed,
            ExpiredTime = accountDto.ExpiredTime,
            ModifiedDate = DateTime.Now
        };
    } 

    public static explicit operator AccountDto(Account account)
    {
        return new AccountDto
        {
            Guid = account.Guid,
            Otp = account.Otp,
            IsUsed = account.IsUsed,
            ExpiredTime = account.ExpiredTime
        };
    }
}
