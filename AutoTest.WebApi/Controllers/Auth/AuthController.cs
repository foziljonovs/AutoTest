using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Users.User;
using AutoTest.BLL.Interfaces.Users;
using Microsoft.AspNetCore.Mvc;

namespace AutoTest.WebApi.Controllers.Auth;

[Route("api/auth")]
[ApiController]
public class AuthController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] LoginDto request)
    {
        try
        {
            var response = await _service.LoginAsync(request);
            return Ok(response);
        }
        catch (StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}

