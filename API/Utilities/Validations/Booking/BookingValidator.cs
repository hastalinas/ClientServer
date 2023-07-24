using API.Contracts;
using API.DTOs.Bookings;
using FluentValidation;

namespace API.Utilities.Validations.Booking;

public class BookingValidator : AbstractValidator<BookingDto>
{
    private readonly IBookingRepository _bookingRepository;
    public BookingValidator(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
        RuleFor(b => b.StartDate).NotEmpty().LessThan(DateTime.Now.AddHours(-2));
        RuleFor(b => b.EndDate).NotEmpty().GreaterThan(DateTime.Now);
        RuleFor(b => b.EmployeeGuid).NotEmpty();
        RuleFor(b => b.RoomGuid).NotEmpty();
        RuleFor(b => b.Remarks).NotEmpty();
        RuleFor(b => b.Status).NotEmpty().IsInEnum();
    }
}
