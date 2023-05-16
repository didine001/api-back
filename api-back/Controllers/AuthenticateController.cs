using Api.Models;
using api_back.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {

        private IUserService userServices;

        public AuthenticationController(IUserService service)
        {

            this.userServices = service;

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

