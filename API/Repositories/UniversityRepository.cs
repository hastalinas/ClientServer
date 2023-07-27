using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class UniversityRepository : GeneralRepository<University>, IUniversityRepository
{
    public UniversityRepository(BookingDBContext context) : base(context) { }

    public University? GetByCode(string code)
    {
        return _context.Set<University>().SingleOrDefault(u => u.Code == code);  
    }
    public IEnumerable<University> GetByName(string name)
    {
        return _context.Set<University>()
                       .Where(university => university.Name.Contains(name))
                       .ToList();
    }

    public bool isNotExist(string value)
    {
        return _context.Set<University>().SingleOrDefault(u => u.Code.Contains(value) || u.Name.Contains(value)) is null;
        //return _context.Set<University>().SingleOrDefault(univ => univ.Name == value||univ.Code == value)is null;
    }

    public Guid GetLastUniversityGuid()
    {
        return _context.Set<University>().ToList().LastOrDefault().Guid;
    }
}
