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

        //Retrieves all menu items, as a list of all menu items
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAllMenuitems()
        {
            var menuItems = await _menuItemService.GetAllMenuItemsAsync();
            return Ok(menuItems);   //returns 200 OK with the list of menu items 
        }

        //Retrieves a specific menu item by its ID
        //Teh menu data if found otherwise 404 Not Found
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetMenuItemById(int id)
        {
            var menuItem = await _menuItemService.GetMenuItemByIdAsync(id);
            if (menuItem == null)
            {
                return NotFound();  //return 404 Not Found if the menu item does not exist
            }
            return Ok(menuItem);    //returns 200 Ok with the menu item data 
        }
        //Creates a new menu item 
        //A 201 Created response with the newly created menu item 
        [HttpPost]
        public async Task<ActionResult> AddMenuItem([FromBody] MenuItemDto menuItem)
        {
            if (menuItem == null)
            {
                return BadRequest();    //Returns 400 Bad request if the input data is invalid 
            }
            await _menuItemService.AddMenuItemAsync(menuItem);
            return CreatedAtAction(nameof(GetMenuItemById), new { id = menuItem.MenuId }, menuItem);    //returns 201 Ok Created
        }

        //Updates an existing menu item 
        //returns 204 No Content if sucessful, otherwise 400 Bad request or 404 Not found 
        [HttpPut("{id}")]
        public async Task<ActionResult>UpdateMenuItem(int id, [FromBody] MenuItemDto menuItem)
        {
            if (menuItem == null || menuItem.MenuId != id)
            {
                return BadRequest();    // Return 400 Bad request if the input data is invalid or Id doesnt match 
            }
            await _menuItemService.UpdateMenuItemAsync(menuItem);
            return NoContent(); //returns 204 No Content on succesfull Update!
        }

        //Deletes a specific menu item by its Id 
        //returns 204 No Content if succesfull, otherwise 404 Not Found 
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMenuItem(int id)
        {
            await _menuItemService.DeleteMenuItemAsync(id);
            return NoContent();     //returns 204 No Content o successful delete.
        }
    }       
}
