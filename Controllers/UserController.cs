using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);  
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound(); 
            }
            return Ok(user);   
        }

        
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<User>> AddUser([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest();    
            }
           var createdUser = await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.UserId }, createdUser);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest();
            }
            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  
            }
            return Ok(user);   
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound(); 
            }
            return Ok();    
        }
    }
}
