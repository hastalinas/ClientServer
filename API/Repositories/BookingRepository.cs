using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    public BookingRepository(BookingDBContext context) : base(context) { }

    public IEnumerable<Booking> GetByName(string remarks)
    {
        return _context.Set<Booking>()
                       .Where(booking => booking.Remarks.Contains(remarks))
                       .ToList();
    }
}
