using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRepository : GeneralRepository<Account>, IAccountRepository
{
    public AccountRepository(BookingDBContext context) : base(context) { }
}
