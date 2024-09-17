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

        //Get: api/Table
        //Retrieves all tables
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAllTables()
        {
            var tables = await _tableService.GetAllTablesAsync();
            return Ok(tables);
        }

        //Get: api/Table{id}
        //Retrieves a specific table by its ID
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

        //Post: api/Table
        //Adds a new table
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

        //Put: api/Table/{id}
        //Update an existing table
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

        //Delete: api/Table/{id}
        //deletes a specific table by its ID
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteTable(int id)
        {
            await _tableService.DeleteTableAsync(id);
            return NoContent();
        }

        //Get: api/Table/available
        //Checks for available tables on a specific date and time
        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAvailableTables([FromQuery] DateOnly date, [FromQuery] TimeOnly time)
        {
            var availableTables = await _tableService.GetAvailableTablesAsync(date, time);
            return Ok(availableTables);
        }

        // GET: api/Table/available/{tableId}
        // Checks if a specific table is available on a specific date and time
        [HttpGet("available/{tableId}")]
        public async Task<ActionResult<bool>> IsTableAvailable(int tableId, [FromQuery] DateOnly date, [FromQuery] TimeOnly time)
        {
            var isAvailable = await _tableService.IsTableAvailableAsync(tableId, date, time);
            return Ok(isAvailable); // Returns true if available, false if not
        }
    }
}
