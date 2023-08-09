using API.Contracts;
using API.Models;
using API.Services;
using API.DTOs.Educations;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Universities;
using API.Utilities.Handlers;
using System.Net;
using API.DTOs.Accounts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace API.Controllers;

[ApiController]
[Route("api/educations")]
//[Authorize]
[EnableCors]
public class EducationController : ControllerBase
{
    private readonly EducationService _educationService;

    public EducationController(EducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _educationService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<EducationDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<EducationDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

/*    [HttpGet("major/{major}")]
    public IActionResult GetByName(string major)
    {
        var result = _educationRepository.GetByName(major);
        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }*/

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _educationService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<EducationDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found",
                Data = result
            });
        }

        return Ok(new ResponseHandler<EducationDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Insert(NewEducationDto newEducationDto)
    {
        var result = _educationService.Create(newEducationDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<EducationDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieve data",
                Data = result
            });
        }


        return Ok(new ResponseHandler<EducationDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data success inserted",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(EducationDto educationDto)
    {
        var result = _educationService.Update(educationDto);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<EducationDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieve data",
                Data = null
            });
            //return StatusCode(500, "Error Retrieve from database");
        }

        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Update success",
            Data = result
        });
    }

    [HttpDelete]
    public IActionResult Delete(Guid guid)
    {
        var result = _educationService.Delete(guid);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<EducationDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Server error",
                Data = null
            });
        }

        return Ok(new ResponseHandler<int>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Delete success",
            Data = result
        });
    }
}
