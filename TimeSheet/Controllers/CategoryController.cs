using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
using System.Threading.Tasks;
using System.Threading;
using System;
using Contracts;

[ApiController]
[Route("api/Category")]
public class CategoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet]
    public async Task<IActionResult> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await _serviceManager.CategoryService.GetAllAsync(cancellationToken);
        return Ok(categories);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetCategoryById(Guid id,CancellationToken cancellationToken)
    {
         var category = await _serviceManager.CategoryService.GetByIdAsync(id,cancellationToken);
        return Ok(category);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategory(Guid id,CancellationToken cancellationToken)
    {
       await _serviceManager.CategoryService.DeleteAsync(id,cancellationToken);
       return NoContent();
    }
    [HttpPost]
    public async Task<IActionResult> PostCategory([FromBody] CategoryDTO categoryDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.CategoryService.CreateAsync(categoryDTO,cancellationToken);
        return Ok();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCategory(Guid id,[FromBody] CategoryDTO categoryDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.CategoryService.UpdateAsync(id,categoryDTO,cancellationToken);
        return NoContent();
    }
    
}