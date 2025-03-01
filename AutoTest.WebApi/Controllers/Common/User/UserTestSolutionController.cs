using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.Users.UserTestSolution;
using AutoTest.BLL.Interfaces.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AutoTest.WebApi.Controllers.Common.User;

[Route("api/user/test-solutions")]
[ApiController, Authorize]
public class UserTestSolutionController(IUserTestSolutionService service) : ControllerBase
{
    private readonly IUserTestSolutionService _service = service;

    [HttpGet("{userId:long}/user")]
    public async Task<IActionResult> GetAllByUserIdAsync([FromRoute] long userId, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.GetAllByUserIdAsync(userId, cancellation);
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

    [HttpGet("{testId:long}/test")]
    public async Task<IActionResult> GetAllByTestIdAsync([FromRoute] long testId, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.GetAllByTestIdAsync(testId, cancellation);
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

    [HttpPost]
    public async Task<IActionResult> AddAsync([FromBody] CreateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.AddUserTestSolutionAsync(dto, cancellation);
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

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateUserTestSolutionDto dto, CancellationToken cancellation = default)
    {
        try
        {
            var response = await _service.UpdateAsync(id, dto, cancellation);
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
}
