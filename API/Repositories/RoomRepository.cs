using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class RoomRepository : GeneralRepository<Room>, IRoomRepository
{
    public RoomRepository(BookingDBContext context) : base(context) { }

    public IEnumerable<Room> GetByName(string floor)
    {
        return _context.Set<Room>()
                       .Where(room => room.Name.Contains(floor))
                       .ToList();
    }
}
