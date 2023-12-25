using Domain.Dtos.Shift;
using Domain.Entities;
using Infrastructure.Context;
using Infrastructure.Services.ShiftService;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class CheckpointController : ControllerBase
{
    private readonly IShiftService _shiftService;
    public CheckpointController(IShiftService shiftService)
    {
        _shiftService = shiftService;
    }

    [HttpGet("AllShift")]
    public async Task<ActionResult> AllShift()
    {
        var result = await _shiftService.AllShift();
        return Ok(result);
    }

    [HttpPost("startshift")]
    public async Task<ActionResult> StartShift(int employeeId, DateTime startTime)
    {
        var result = await _shiftService.StartShift(employeeId, startTime);
        return Ok(result);
    }

    [HttpPost("endshift")]
    public async Task<ActionResult> EndShift(int employeeId, DateTime endTime, AddShiftDto shift)
    {
        var result = await _shiftService.EndShift(employeeId, endTime, shift);
        return Ok(result);
    }
}
