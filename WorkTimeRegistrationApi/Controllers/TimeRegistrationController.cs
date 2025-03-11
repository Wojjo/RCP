using Microsoft.AspNetCore.Mvc;
using WorkTimeRegistrationApi.Service.TimeRegistrationService;
using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApi.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class TimeRegistrationController : ControllerBase
{
    private readonly ITimeRegistrationService _timeRegistrationService;
    public TimeRegistrationController(ITimeRegistrationService timeRegistrationService)
    {
        _timeRegistrationService = timeRegistrationService;
    }

    [HttpGet("{userId}/{date}")]
    public async Task<IActionResult> GetRegisteredTime(int userId, DateTime date)
    {
        var result = await _timeRegistrationService.GetRegisteredTime(userId, date);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> RegisterTime(RegisterTimeDto workTime)
    {
        var result = await _timeRegistrationService.RegisterTime(workTime);
        if(result.IsSuccess == false)
        {
            return BadRequest();
        }
        return Ok(result);
    }
}
