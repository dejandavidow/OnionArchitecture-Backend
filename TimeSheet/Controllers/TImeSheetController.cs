using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
[ApiController]
[Route("api/TimeSheet")]
public class TimeSheetController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public TimeSheetController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [Authorize]
    [HttpGet("filters-count")]
    public async Task<IActionResult> GetFilterTimeSheetsCount([FromQuery] TimeSheetParams timesheetParams)
    {
        var timesheets = await _serviceManager.TimeSheetService.GetCount(timesheetParams);
        return Ok(timesheets);
    }
    [Authorize]
    [HttpGet("filters")]
    public async Task<IActionResult> GetFilterTimeSheets([FromQuery] TimeSheetParams timesheetParams, CancellationToken cancellationToken)
    {
        var timesheets = await _serviceManager.TimeSheetService.GetFilteredTS(timesheetParams, cancellationToken);
        return Ok(timesheets);
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> GetTimeSheets([FromQuery] FetchParams fetchParams, CancellationToken cancellationToken)
    {
        var timesheets = await _serviceManager.TimeSheetService.GetAllAsync(fetchParams,cancellationToken);
        return Ok(timesheets);
    }
    [Authorize]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTimeSheetById(Guid id,CancellationToken cancellationToken)
    {
    var timesheet = await _serviceManager.TimeSheetService.GetByIdAsync(id,cancellationToken);
    return Ok(timesheet);
    }
    [Authorize]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTimeSheet(Guid id,CancellationToken cancellationToken)
    {
        await _serviceManager.TimeSheetService.DeleteAsync(id,cancellationToken);
        return NoContent();
    }
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> PostSheet([FromBody] CreateTimeSheetDTO timeSheetDTO)
    {
        await _serviceManager.TimeSheetService.CreateAsync(timeSheetDTO);
        return Ok();
    }
    [Authorize]
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSheet(Guid id,[FromBody] CalendarTsDTO timeSheetDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.TimeSheetService.UpdateAsync(id,timeSheetDTO,cancellationToken);
        return NoContent();
    }
}