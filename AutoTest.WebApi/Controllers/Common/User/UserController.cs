using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.User;
using AutoTest.BLL.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AutoTest.WebApi.Controllers.Common.User;

[Route("api/users")]
[ApiController]
public class UserController(IUserService service) : ControllerBase
{
    private readonly IUserService _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.GetAllAsync(cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] long id, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.GetByIdAsync(id, cancellation);
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

    [HttpGet("{id:long}/verify")]
    public async Task<IActionResult> VerifyPasswordAsync([FromRoute] long id, [FromQuery] string password, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.VerifyPasswordAsync(id, password, cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> RegisterAsync([FromBody] RegisterDto request, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.RegisterAsync(request, cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost("{id:long}/change-password")]
    public async Task<IActionResult> ChangePasswordAsync([FromRoute] long id, [FromBody] UserChangePasswordDto request, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.ChangePasswordAsync(id, request, cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateUserDto request, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.UpdateAsync(id, request, cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
        {
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] long id, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.DeleteAsync(id, cancellation);
            return Ok(response);
        }
        catch(StatusCodeException ex)
{
            return StatusCode((int)ex.StatusCode, ex.Message);
        }
        catch(Exception ex)
    {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
