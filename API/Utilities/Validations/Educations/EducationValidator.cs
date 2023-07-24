using API.Contracts;
using API.DTOs.Educations;
using FluentValidation;

namespace API.Utilities.Validations.Educations;

public class EducationValidator : AbstractValidator<EducationDto>
{
    private readonly IEducationRepository _educationRepository;
    public EducationValidator(IEducationRepository educationRepository)
    {
        _educationRepository = educationRepository;
        RuleFor(ed => ed.Major).NotEmpty();
        RuleFor(ed => ed.GPA).NotEmpty();
        RuleFor(ed => ed.Degree).NotEmpty();
    }
}
