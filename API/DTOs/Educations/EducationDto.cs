using API.Models;

namespace API.DTOs.Education;

public class EducationDto
{
    public Guid Guid { get; set; }
    public string Major { get; set; }
    public string Degree { get; set; }
    public float GPA { get; set; }
    public string UniversityGuid { get; set; }

    public static implicit operator Education(EducationDto educationDto)
    {
        return new Education
        {
            Guid = educationDto.Guid,
            Major = educationDto.Major,
            Degree = educationDto.Degree,
            GPA = educationDto.GPA,
            UniversityGuid = educationDto.UniversityGuid
        };
    }

    public static explicit operator EducationDto(Education education)
    {
        return new EducationDto
        {
            Guid = education.Guid,
            Major = education.Major,
            Degree = education.Degree,
            GPA = education.GPA,
            UniversityGuid = education.UniversityGuid
        };
    }
}
