using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Contracts;

[ApiController]
[Route("api/Project")]
public class ProjectController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ProjectController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetProjects(CancellationToken cancellationToken)
    {
        var projects = await _serviceManager.ProjectService.GetAllAsync(cancellationToken);
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
    public async Task<IActionResult> PostProject([FromBody] ProjectDTO projectDTO)
    {
        await _serviceManager.ProjectService.CreateAsync(projectDTO);
        return Ok();
    }
    [HttpPut("{Id}")]
    public async Task<IActionResult> UpdateProject(Guid id,[FromBody] ProjectDTO projectDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.ProjectService.UpdateAsync(id,projectDTO,cancellationToken);
        return NoContent();
    }
}