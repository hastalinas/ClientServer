using API.DTOs.Rooms;
using API.DTOs.Bookings;
using API.Models;

namespace API.Contracts;

public interface IBookingRepository : IGeneralRepository<Booking>
{
    IEnumerable<Booking> GetByName(string remarks);

}
