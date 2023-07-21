using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class AccountRoleRepository : GeneralRepository<AccountRole>, IAccounRoleRepository
{
    public AccountRoleRepository(BookingDBContext context) : base(context) { }


}
