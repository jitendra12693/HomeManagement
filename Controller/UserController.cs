using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }   
      
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userRepository.GetAllUsersAsync();
            return Ok(users);
        }

        [HttpPost("{departmentId}/addUser")]
        public async Task<IActionResult> AddUser([FromBody] Model.User user,int departmentId)
        {
            
            user.DepartmentId = departmentId;
            var newUser = await _userRepository.AddUserAsync(user);
            if(newUser == null)
            {
                return BadRequest("User with this email already exists");
            }
            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }
        // {
        //     var newUser = await _userRepository.AddUserAsync(user);
        //     if(newUser == null)
        //     {
        //         return BadRequest("User with this email already exists");
        //     }
        //     //return Ok(newUser);
        //     return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        // }

        [HttpGet("getUserById/{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userRepository.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }   
    }
}