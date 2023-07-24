using API.Contracts;
using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

public class NewEducationValidator : AbstractValidator<NewEducationDto>
{
    private readonly IEducationRepository _educationRepository;
    public NewEducationValidator(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
        RuleFor(ed => ed.Major).NotEmpty();
        RuleFor(ed => ed.GPA).NotEmpty();
        RuleFor(ed => ed.Degree).NotEmpty();
    }
}
