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
[Route("api/Category")]
public class CategoryController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public CategoryController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet("search-count")]
    public async Task<IActionResult> SearchCountCategories([FromQuery] string search)
    {
        var categories = await _serviceManager.CategoryService.searchCountAsync(search);
        return Ok(categories);
    }
    [HttpGet("count")]
    public async Task<IActionResult> FilterCountCategories([FromQuery] string letter)
    {
        var categories = await _serviceManager.CategoryService.FilterCountAsync(letter);
        return Ok(categories);
    }

    [HttpGet("filter")]
    public async Task<IActionResult> FilterCategories([FromQuery] CategoryParams categoryParams, string letter)
    {
        var categories = await _serviceManager.CategoryService.FilterAsync(categoryParams,letter);
        return Ok(categories);
    }
    [HttpGet("search")]
    public async Task<IActionResult> SearchCategories([FromQuery] CategoryParams categoryParams,string search)
    {
        var categories = await _serviceManager.CategoryService.SearchAsync(categoryParams,search);
        return Ok(categories);
    }
    [HttpGet]
    public async Task<IActionResult> GetCategories([FromQuery] CategoryParams categoryParams,CancellationToken cancellationToken)
    {
        var categories = await _serviceManager.CategoryService.GetAllAsync(categoryParams,cancellationToken);
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