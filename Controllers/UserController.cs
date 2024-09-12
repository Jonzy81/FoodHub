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

        //Get all users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);   //Returns ok with user 
        }

        //Get User by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();  //Returns not found
            }
            return Ok(user);    //Returns ok with user
        }

        //Create user
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult> AddUser([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest();    //Returns 400 if input is invalid
            }
            await _userService.AddUserAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user); //Returns created
        }

        //Update User
        [HttpPut("{id}")]
        //[Route("UpdateUser")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody] UserDto user)
        {
            if (user == null || user.UserId != id)
            {
                return BadRequest(); //Returns bad request if input is invalid
            }
            try
            {
                await _userService.UpdateUserAsync(user);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  //returns 404 not found if user nit found
            }
            return Ok(user);    //returns Ok if success
        }

        //Delete User
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(int id)
        {
            try
            {
                await _userService.DeleteUserAsync(id);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();  //Returns 404 if user doesnt exist
            }
            return Ok();    //Returns ok if success
        }
    }
}
