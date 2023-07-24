using API.Contracts;
using API.DTOs.Rooms;
using FluentValidation;

namespace API.Utilities.Validations.Rooms;

public class RoomValidator : AbstractValidator<RoomDto>
{
    private readonly IRoomRepository _roomRepository;
    public RoomValidator(IRoomRepository roomRepository)
    {
        _roomRepository = roomRepository;
        RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required")
            .Must(IsDuplicateValue).WithMessage("Name is duplicated");
    }

    private bool IsDuplicateValue(string arg)
    {
        return _roomRepository.isNotExist(arg);
    }
}
