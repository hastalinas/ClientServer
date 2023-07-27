using API.Models;

namespace API.Contracts;

public interface IAccountRepository /*: IApiRepository<Account>*/
{
    IEnumerable<Account> GetAll();
    Account? GetByGuid(Guid id);
    Account? Create(Account account);
    bool Update(Account account);
    bool Delete(Account account);
    
}
