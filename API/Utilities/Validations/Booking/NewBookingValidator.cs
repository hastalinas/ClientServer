using API.Contracts;
using API.DTOs.Bookings;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Booking;

public class NewBookingValidator : AbstractValidator<NewBookingDto>
{
    private readonly IBookingRepository _bookingRepository;
    public NewBookingValidator(IBookingRepository bookingRepository)
    {
        _bookingRepository = bookingRepository;
        RuleFor(booking => booking.StartDate)
            .NotEmpty().WithMessage("Start Date can not be Null")
            .Must(WorkingHours).WithMessage("Must Be On Working Hours Between 09.00 to 17.00");
        RuleFor(booking => booking.EndDate)
            .NotEmpty().WithMessage("End Date can not be Null")
            .GreaterThanOrEqualTo(booking => booking.StartDate.AddHours(1))
            .Must(WorkingHours).WithMessage("Must Be On Working Hours Between 09.00 to 17.00"); ;
        RuleFor(b => b.EmployeeGuid).NotEmpty();
        RuleFor(b => b.RoomGuid).NotEmpty();
        RuleFor(b => b.Remarks).NotEmpty();
        RuleFor(b => b.Status).NotEmpty().IsInEnum();
    }

    private bool WorkingHours(DateTime time)
    {

        var start = new TimeSpan(09, 00, 00);
        var end = new TimeSpan(17, 00, 00);
        TimeSpan currenttime = time.TimeOfDay;
        return currenttime >= start && currenttime <= end;
    }
}
