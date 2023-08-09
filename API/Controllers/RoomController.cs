using API.Contracts;
using API.DTOs.Accounts;
using API.DTOs.Rooms;
using API.DTOs.Universities;
using API.Models;
using API.Repositories;
using API.Services;
using API.Utilities.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/rooms")]
//[Authorize(Roles = "Manager")]
[EnableCors]
public class RoomController : ControllerBase
{
    private readonly RoomService _roomService;

    public RoomController(RoomService roomService)
    {
        _roomService = roomService;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _roomService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<RoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

/*    [HttpGet("room/{room}")]
    public IActionResult GetByName(string room)
    {
        var result = _roomService.GetByName(room);
        if (!result.Any())
        {
            return NotFound();
        }

        return Ok(result);
    }*/

    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _roomService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found",
                Data = result
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Insert(NewRoomDto newRoomDto)
    {
        var result = _roomService.Create(newRoomDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<RoomDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieve data",
                Data = result
            });
        }

        return Ok(new ResponseHandler<RoomDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data success inserted",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(RoomDto roomDto)
    {
        var result = _roomService.Update(roomDto);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoomDto>
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
        var result = _roomService.Delete(guid);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<RoomDto>
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

    [HttpGet("bookedtoday")]
    public IActionResult GetBookedRoomToday()
    {
        var result = _roomService.GetAllBookedRoomToday();
        if (result is null)
        {
            return NotFound(new ResponseHandler<BookedRoomDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<BookedRoomDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }
}
