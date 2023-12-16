using Microsoft.AspNetCore.Mvc;
using Services.Abstractions;
namespace TimeSheet.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TimesheetsController : ControllerBase
    {
        private readonly ITimeSheetService _timeSheetService;
        public TimesheetsController(ITimeSheetService timeSheetService)
        {
            _timeSheetService = timeSheetService;
        }
        [HttpPost]
        public IActionResult Post(Domain.Entities.TimeSheet timeSheet)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _timeSheetService.Create(timeSheet);
            return CreatedAtAction(nameof(Get), new { id = timeSheet.Id }, timeSheet);
        }
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var timesheet = _timeSheetService.GetOne(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            return Ok(timesheet);
        }
        [HttpGet]
        public IActionResult Get([FromQuery] int pagenumber = 1, int pagesize = 5)
        {
            return Ok(_timeSheetService.GetAll(pagenumber, pagesize));
        }
        [HttpPut("{id}")]
        public IActionResult Put(int id, Domain.Entities.TimeSheet timeSheet)
        {
            if (id != timeSheet.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            _timeSheetService.Update(timeSheet);
            return Ok(timeSheet);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var timesheet = _timeSheetService.GetOne(id);
            if (timesheet == null)
            {
                return NotFound();
            }
            _timeSheetService.Delete(timesheet);
            return NoContent();
        }
    }
}
