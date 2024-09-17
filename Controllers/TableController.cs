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
    }
}
