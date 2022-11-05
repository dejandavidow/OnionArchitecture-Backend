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
[Route("api/Clients")]
public class ClientController : ControllerBase
{
    private readonly IServiceManager _serviceManager;
    public ClientController(IServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
    }
    [HttpGet("filter-count")]
    public async Task<IActionResult> GetCountFilteredClients([FromQuery] string letter)
    {
      var clients = await _serviceManager.ClientService.GetFilterCount(letter);
      return Ok(clients);
    }

    [HttpGet("filters")]
    public async Task<IActionResult> GetFilterClients([FromQuery] ClientParams clientParams)
    {
        var clients = await _serviceManager.ClientService.GetFilterAsync(clientParams);
        return Ok(clients);
    }

    [HttpGet("search-count")]
    public async Task<IActionResult> GetCountClients([FromQuery] string search)
    {
      var clients = await _serviceManager.ClientService.GetAllCount(search);
      return Ok(clients);
    }


    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ClientParams clientParams ,CancellationToken cancellation)
    {
        var clients = await _serviceManager.ClientService.GetAllAsync(clientParams,cancellation);
        return Ok(clients);
    }
    [HttpGet("search/{search}")]
    public async Task<IActionResult> SearchClients([FromQuery] ClientParams clientParams,string search,CancellationToken cancellationToken)
    {
        var clients = await _serviceManager.ClientService.GetSearchAsync(clientParams,search,cancellationToken);
        return Ok(clients);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetClientById(Guid id,CancellationToken cancellationToken)
    {
        var client = await _serviceManager.ClientService.GetByIdAsync(id,cancellationToken);
        return Ok(client);
    }
    [HttpPost]
    public async Task<IActionResult> PostClient([FromBody] ClientDTO clientDTO, CancellationToken cancellationToken)
    {
        await _serviceManager.ClientService.CreateAsync(clientDTO,cancellationToken);
        return Ok();
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteClient(Guid id,CancellationToken cancellationToken)
    {
        await _serviceManager.ClientService.DeleteAsync(id,cancellationToken);
        return NoContent();
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateClient(Guid id,[FromBody] ClientDTO clientDTO,CancellationToken cancellationToken)
    {
        await _serviceManager.ClientService.UpdateAsync(id,clientDTO,cancellationToken);
        return NoContent();
    }
}