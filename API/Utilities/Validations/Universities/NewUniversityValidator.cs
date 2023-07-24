using API.Contracts;
using API.DTOs.Universities;
using API.Repositories;
using FluentValidation;

namespace API.Utilities.Validations.Universities;

public class NewUniversityValidator : AbstractValidator<NewUniversityDto>
{
    private readonly IUniversityRepository _universityRepository;
    public NewUniversityValidator(IUniversityRepository universityRepository)
    {
        _universityRepository = universityRepository;
        RuleFor(u => u.Code).NotEmpty().WithMessage("Code is required")
                            .Must(IsDuplicateValue).WithMessage("Code is dupclicated");

        RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required")
            .Must(IsDuplicateValue).WithMessage("Name is required");
    }

    private bool IsDuplicateValue(string arg)
    {
        return _universityRepository.isNotExist(arg);
    }
}
