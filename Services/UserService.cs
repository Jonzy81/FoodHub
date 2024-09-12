using FoodHub.Data.Repository.IRepository;
using FoodHub.Model;
using FoodHub.Model.Dtos;
using FoodHub.Services.IServices;
using System.Runtime.InteropServices;

namespace FoodHub.Services
{
    public class UserService: IUserService 
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        //adds a new User 
        public async Task AddUserAsync(UserDto user)
        {
            var newUser = new User
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserEmail = user.Email,
                UserPhoneNumber = user.UserPhoneNumber
            };
            await _userRepository.AddUserAsync(newUser);
        }
        //delete user by id 
        public async Task DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);

            if (user == null) 
            {
                //User not found
                throw new Exception($"user with ID {userId} was not found");
            }
            await _userRepository.DeleteUserAsync(userId);
        }
        //retrieve a List of all users 
        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var userList = await _userRepository.GetAllUsersAsync();
            return userList.Select(x => new UserDto
            {
                UserId = x.UserId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Email = x.UserEmail,
                UserPhoneNumber= x.UserPhoneNumber
            }).ToList();
        }
        //Get all user by id
        public async Task<UserDto> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return null;
            }
            return new UserDto
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.UserEmail,
                UserPhoneNumber = user.UserPhoneNumber
            };

        }

        public async Task UpdateUserAsync(UserDto user)
        {
            //retrieve the existing user from repository 
            var existingUser = await _userRepository.GetUserByIdAsync(user.UserId);

            //Check if it exists
            if (existingUser == null) 
            {
                throw new KeyNotFoundException($"User with ID {user.UserId} was not found");
            }
            //Update the user 
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.UserEmail = user.Email;
            existingUser.UserPhoneNumber = user.UserPhoneNumber;

            await _userRepository.UpdateUserAsync(existingUser);
        }
    }
}
