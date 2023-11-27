using Contracts.Pagination;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;

namespace TimeSheet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        private readonly IProjectService _projectService;
        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] QueryParameters parameters)
        {
            return Ok(_projectService.GetAll(parameters));
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var project = _projectService.GetOne(id);
            if (project == null)
            {
                return NotFound();
            }
            return Ok(project);
        }
        [HttpPost]
        public IActionResult Post(Project project)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _projectService.Create(project);
            return CreatedAtAction(nameof(Get), new { id = project.Id }, project);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Project project)
        {
            if (id != project.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _projectService.Update(project);
            return Ok(project);
        }
        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var project = _projectService.GetOne(id);
            if (project == null)
            {
                return NotFound();
            }
            _projectService.Delete(project);
            return NoContent();
        }
    }
}
