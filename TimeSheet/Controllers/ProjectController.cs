using System;
using System.Threading;
using System.Threading.Tasks;
using Contracts.DTOs;
using Domain.Pagination;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
[Authorize]
[ApiController]
[Route("api/Projects")]
public class ProjectController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProjectController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet("search-count")]
    public async Task<IActionResult> CountSearchProjects([FromQuery] string search)
    {
        var projects = await _serviceManager.ProjectService.CountSearchProjects(search);
        return Ok(projects);
    }
    [HttpGet("filter-count")]
    public async Task<IActionResult> CountFilterProjects([FromQuery] string letter)
    {
        var projects = await _serviceManager.ProjectService.CountFilterProjects(letter);
        return Ok(projects);
    }
    [HttpGet("search")]
    public async Task<IActionResult> SearchProjects([FromQuery] ProjectParams projectParams, string search)
    {
        var projects = await _serviceManager.ProjectService.FilterProjects(projectParams, search);
        return Ok(projects);
    }
    [HttpGet("filters")]
    public async Task<IActionResult> FilterProjects([FromQuery] ProjectParams projectParams,string letter)
    {
        var projects = await _serviceManager.ProjectService.FilterProjects(projectParams,letter);
        return Ok(projects);
    }
    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] ProjectParams projectParams,CancellationToken cancellationToken)
    {
        var projects = await _serviceManager.ProjectService.GetAllAsync(projectParams,cancellationToken);
        return Ok(projects);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(Guid id,CancellationToken cancellationToken)
    {
        var project = await _serviceManager.ProjectService.GetByIdAsync(id,cancellationToken);
        return Ok(project);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id,CancellationToken cancellationToken)
    {
        await _serviceManager.ProjectService.DeleteAsync(id,cancellationToken);
        return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> PostProject([FromBody] PostProjectDTO projectDTO)
    {
        await _serviceManager.ProjectService.CreateAsync(projectDTO);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id,[FromBody] PostProjectDTO projectDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.ProjectService.UpdateAsync(id,projectDTO,cancellationToken);
        return NoContent();
    }
}