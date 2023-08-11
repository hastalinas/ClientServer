using API.DTOs.Employees;
using API.DTOs.Rooms;
using API.Models;

namespace Client.Contracts;

public interface IRoomRepository : IRepository<Room, Guid>
{
}
