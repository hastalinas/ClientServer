using API.Contracts;
using API.Models;
using API.Repositories;
using API.Services;
using API.DTOs.Accounts;
using Microsoft.AspNetCore.Mvc;
using API.DTOs.Universities;
using API.Utilities.Handlers;
using System.Net;

namespace API.Controllers;

[ApiController]
[Route("api/accounts")]
public class AccountController : ControllerBase
{
    private readonly AccountService _accountService;

    public AccountController(AccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("login")]
    public IActionResult Login(LoginDto loginDto)
    {
        var result = _accountService.Login(loginDto);

        if (result is 0)
        {
            return NotFound(new ResponseHandler<LoginDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Email or Password is incorrect"
            });
        }

        return Ok(new ResponseHandler<LoginDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Login Success"
        });
    }

    [HttpPost("register")]
    public IActionResult Register(RegisterDto registerDto)
    {
        var result = _accountService.Register(registerDto);
        if (result is null)
        {
            return NotFound(new ResponseHandler<RegisterDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Register failed"
            });
        }
        return Ok(new ResponseHandler<RegisterDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Register Success"
        });
    }

/*    [HttpPost("ForgotPassword")]
    public IActionResult ForgotPassword([FromBody] ForgotPasswordRequest request)
    {
        // Simulate generating a random 6-digit OTP
        string otp = GenerateRandomOTP();

        OTPData.Add(request.Email, new OTPInfo { OTP = otp, ExpirationTime = DateTime.UtcNow.AddMinutes(5) });

        // Return the OTP in the response body
        return Ok(new { OTP = otp });
    }


    [HttpPost("ChangePassword")]
    public IActionResult ChangePassword([FromBody] ChangePasswordRequest request)
    {
        // Check if the OTP exists for the given email
        if (!OTPData.TryGetValue(request.Email, out var otpInfo) || otpInfo.OTP != request.OTP)
        {
            return BadRequest("Invalid OTP");
        }

        if (otpInfo.Used)
        {
            return BadRequest("OTP has already been used");
        }

        if (DateTime.UtcNow > otpInfo.ExpirationTime)
        {
            return BadRequest("OTP has expired");
        }

        if (request.NewPassword != request.ConfirmPassword)
        {
            return BadRequest("New password and confirm password do not match");
        }


        otpInfo.Used = true;

        return Ok("Password changed successfully");
    }*/

    [HttpGet]
    public IActionResult GetAll()
    {
        var result = _accountService.GetAll();
        if (!result.Any())
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Data not found"
            });
        }

        return Ok(new ResponseHandler<IEnumerable<AccountDto>>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }


    [HttpGet("{guid}")]
    public IActionResult GetByGuid(Guid guid)
    {
        var result = _accountService.GetByGuid(guid);
        if (result is null)
        {
            return NotFound(new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status404NotFound,
                Status = HttpStatusCode.NotFound.ToString(),
                Message = "Guid is not found",
                Data = result
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Success retrieve data",
            Data = result
        });
    }

    [HttpPost]
    public IActionResult Insert(NewAccountDto newAccountDto)
    {
        var result = _accountService.Create(newAccountDto);
        if (result is null)
        {
            return StatusCode(500, new ResponseHandler<AccountDto>
            {
                Code = StatusCodes.Status500InternalServerError,
                Status = HttpStatusCode.InternalServerError.ToString(),
                Message = "Error retrieve data",
                Data = result
            });
        }

        return Ok(new ResponseHandler<AccountDto>
        {
            Code = StatusCodes.Status200OK,
            Status = HttpStatusCode.OK.ToString(),
            Message = "Data success inserted",
            Data = result
        });
    }

    [HttpPut]
    public IActionResult Update(AccountDto accountDto)
    {
        var result = _accountService.Update(accountDto);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<AccountDto>
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
        var result = _accountService.Delete(guid);
        if (result is -1)
        {
            return NotFound("Guid is not found");
        }

        if (result is 0)
        {
            return StatusCode(500, new ResponseHandler<AccountDto>
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
