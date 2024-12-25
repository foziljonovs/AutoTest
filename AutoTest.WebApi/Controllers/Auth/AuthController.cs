using AutoTest.BLL.Common.Exceptions;
using AutoTest.BLL.DTOs.User;
using AutoTest.BLL.Interfaces.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AutoTest.WebApi.Controllers.Auth
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IUserService service) : ControllerBase
    {
        private readonly IUserService _service = service;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginDto dto)
        {
            try
            {
                var result = await _service.LoginAsync(dto);
                return Ok(result);
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
}
