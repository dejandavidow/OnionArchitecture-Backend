using Contracts.Pagination;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Abstractions;

namespace TimeSheet.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        public ClientsController(IClientService clientservice)
        {
            _clientService = clientservice;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] QueryParameters parameters)
        {
            var clients = _clientService.GetAll(parameters);
            var metadata = new
            {
                clients.TotalCount,
                clients.PageSize,
                clients.CurrentPage,
                clients.TotalPages,
                clients.HasNext,
                clients.HasPrevious
            };
            Response.Headers.Append("X-Pagination", JsonConvert.SerializeObject(metadata));
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var client = _clientService.GetOne(id);
            if (client == null)
            {
                return NotFound();
            }
            return Ok(client);
        }
        [HttpPost]
        public IActionResult Post(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _clientService.Create(client);
            return CreatedAtAction(nameof(Get), new { id = client.Id }, client);
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Client client)
        {
            if (id != client.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _clientService.Update(client);
            return Ok(client);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var client = _clientService.GetOne(id);
            if (client == null)
            {
                return NotFound();
            }
            _clientService.Delete(client);
            return NoContent();
        }
    }
}
