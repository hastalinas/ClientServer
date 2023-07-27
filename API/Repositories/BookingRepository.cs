using API.Contracts;
using API.Data;
using API.DTOs.Bookings;
using API.DTOs.Rooms;
using API.Models;
using API.Utilities.Enums;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Xml.Linq;

namespace API.Repositories;

public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
{
    private readonly IRoomRepository _roomRepository;
    public BookingRepository(BookingDBContext context, IRoomRepository roomRepository) : base(context) 
    { 
        _roomRepository = roomRepository;
    }

    public IEnumerable<Booking> GetByName(string remarks)
    {
        return _context.Set<Booking>()
                       .Where(booking => booking.Remarks.Contains(remarks))
                       .ToList();
    }

}
