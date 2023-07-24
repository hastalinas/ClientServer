using API.Contracts;
using API.DTOs.Rooms;
using API.Repositories;
using FluentValidation;
using System.Data;

namespace API.Utilities.Validations.Rooms;

public class NewRoomValidator : AbstractValidator<NewRoomDto>
{
    private readonly IRoomRepository _roomRepository;
    public NewRoomValidator(IRoomRepository roomRepository)
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
