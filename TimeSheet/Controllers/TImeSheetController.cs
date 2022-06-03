using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using Contracts;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("api/TimeSheet")]
public class TimeSheetController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public TimeSheetController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetTimeSheets(CancellationToken cancellationToken)
    {
        var timesheets = await _serviceManager.TimeSheetService.GetAllAsync(cancellationToken);
        return Ok(timesheets);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeSheetById(Guid id,CancellationToken cancellationToken)
    {
    var timesheet = await _serviceManager.TimeSheetService.GetByIdAsync(id,cancellationToken);
    return Ok(timesheet);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeSheet(Guid id,CancellationToken cancellationToken)
    {
        await _serviceManager.TimeSheetService.DeleteAsync(id,cancellationToken);
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> PostSheet([FromBody] TimeSheetDTO timeSheetDTO)
    {
        await _serviceManager.TimeSheetService.CreateAsync(timeSheetDTO);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSheet(Guid id,[FromBody] TimeSheetDTO timeSheetDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.TimeSheetService.UpdateAsync(id,timeSheetDTO,cancellationToken);
        return NoContent();
    }
}