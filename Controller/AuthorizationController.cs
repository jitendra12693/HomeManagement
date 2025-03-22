using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomeManagement.Model;
using HomeManagement.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace HomeManagement.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorizationController : ControllerBase
    {
        ILoginRepository _loginRepository;
        public AuthorizationController(ILoginRepository loginRepository)
        {
            _loginRepository = loginRepository;   
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            try
            {
                var token = await _loginRepository.LoginAsync(loginModel);
                return Ok(token);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}