using API.Contracts;
using API.Controllers;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class EducationRepository : GeneralRepository<Education>, IEducationRepository
{
    public EducationRepository(BookingDBContext context) : base(context) { }

    public IEnumerable<Education> GetByName(string major)
    {
        return _context.Set<Education>()
                       .Where(education => education.Major.Contains(major))
                       .ToList();
    }
}
