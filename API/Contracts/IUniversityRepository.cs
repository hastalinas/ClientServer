using API.Models;

namespace API.Contracts;

public interface IUniversityRepository : IGeneralRepository<University> 
{
    IEnumerable<University> GetByName(string name);
    Guid GetLastUniversityGuid();
    bool isNotExist(string value);

    University? GetByCode(string code); // panggil method yg dibuat di repository
}
