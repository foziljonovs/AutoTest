using AutoTest.BLL.DTOs.Tests.Test;
using AutoTest.BLL.Interfaces.OpenAI;
using Microsoft.AspNetCore.Mvc;

namespace AutoTest.WebApi.Controllers.Common.OpenAI;

[Route("api/ai")]
[ApiController]
public class AiController(IOpenAIService service) : ControllerBase
{
    private readonly IOpenAIService _service = service;

    [HttpPost("complete")]
    public async Task<IActionResult> CompleteAsync([FromBody] GenerateTestDto dto)
    {
        try
        {
            var testId = await _service.CompleteAsync(dto);
            return Ok(testId);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}
