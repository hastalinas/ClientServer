using API.Models;

namespace API.Contracts;

public interface IRoomRepository : IGeneralRepository<Room>
{
    IEnumerable<Room> GetByName(string room);
    bool isNotExist(string value);
}
