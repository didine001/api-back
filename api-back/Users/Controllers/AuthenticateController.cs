using api_back.Users.Models;
using api_back.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_back.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private IUserService userServices;

        public AuthenticationController(IUserService service)
        {

            userServices = service;

        }

        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] CreateUserDto credentials)
        {

            List<User> registrations = userServices.GetAll();
            bool isAuthValid = registrations.Any(r => r.Username == credentials.Username && r.Password == credentials.Password);
            if (isAuthValid)
            {
                return Ok(new { Success = true });
            }
            else
            {
                return Unauthorized(new { Success = false, Message = "Invalid username or password" });
            }
        }
    }
}

