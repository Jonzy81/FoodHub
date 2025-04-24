using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

  
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> GetTablebyId(int id)
        {
            var table = await _tableService.GetTablebyIdAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            return Ok(table);
        }

        [HttpPost]
        public async Task<ActionResult> AddTable([FromBody] TableDto tableDto)
        {
            if (tableDto == null)
            {
                return BadRequest();
            }
            await _tableService.AddTableAsync(tableDto);
            return CreatedAtAction(nameof(GetTablebyId), new { id = tableDto.TableId }, tableDto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateTable(int id, [FromBody] TableDto tableDto)
        {
            if (tableDto == null || tableDto.TableId != id)
            {
                return BadRequest();
            }

            await _tableService.UpdatetableAsync(tableDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAvailableTables(
        [FromQuery] DateOnly date,
        [FromQuery] TimeOnly time,
        [FromQuery] int numberOfSeats)
        {
            var availableTables = await _tableService.GetAvailableTablesAsync(date, time, numberOfSeats);
            return Ok(availableTables);
        }

        [HttpGet("available/{tableId}")]
        public async Task<ActionResult<bool>> IsTableAvailable(int tableId, [FromQuery] DateOnly date, [FromQuery] TimeOnly time)
        {
            var isAvailable = await _tableService.IsTableAvailableAsync(tableId, date, time);
            return Ok(isAvailable); 
        }

        [HttpGet("available-dates")]
        public async Task<ActionResult<IEnumerable<DateOnly>>> GetAvailableDates([FromQuery] int numberOfSeats)
        {
            var availableDates = await _tableService.GetAvailableDatesAsync(numberOfSeats);
            return Ok(availableDates);
        }

        [HttpGet("available-times")]
        public async Task<ActionResult<IEnumerable<TimeOnly>>> GetAvailableTimes(
        [FromQuery] DateOnly date,
        [FromQuery] int numberOfSeats)
        {
            var times = await _tableService.GetAvailableTimesAsync(date, numberOfSeats);
            return Ok(times);
        }
    }
}
