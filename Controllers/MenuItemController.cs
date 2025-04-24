using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAllMenuitems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();  
            }
            return Ok(menuItem);    
        }
        
        [HttpPost]
        public async Task<ActionResult> AddMenuItem([FromBody] MenuItemDto menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();   
            }
            await _menuItemService.AddMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuId }, menuItem);   
        }

        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateMenuItem(int id, [FromBody] MenuItemDto menuItem)
        {
            if (menuItem == null || menuItem.MenuId != id)
            {
                return BadRequest();   
            }
            await _menuItemService.UpdateMenuItemAsync(menuItem);
            return NoContent(); 
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenuItem(int id)
        {
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();    
        }
    }       
}
