using Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody] Credentials credentials)
        {
            List<RegistrationInfo> registrations = LoadRegistrations();
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


        [HttpPost("Register")]
        public IActionResult Register([FromBody] RegistrationInfo registrationInfo)
        {
            List<RegistrationInfo> registrations = LoadRegistrations();
            registrationInfo.Id = registrations.Max(u => u.Id) + 1;
            bool isValidRegistration = registrations.All(r => r.Username == registrationInfo.Username || r.Email == registrationInfo.Email);
            if (!isValidRegistration)
            {
                registrations.Add(registrationInfo);
                string Data = JsonConvert.SerializeObject(registrations);
                System.IO.File.WriteAllText("Data/Users.json", Data); 
                return Ok(new { Success = true });
            }
            else
            {
                return Ok(new { Success = false, Message = "The user is already in the database" });
            }
        }

        private List<RegistrationInfo> LoadRegistrations()
        {
            string data = System.IO.File.ReadAllText("Data/Users.json");
            if (string.IsNullOrEmpty(data))
            {
                return new List<RegistrationInfo>(); 
            }
            List<RegistrationInfo> registrations = JsonConvert.DeserializeObject<List<RegistrationInfo>>(data);
            return registrations;
        }
    }

    public class RegistrationInfo
    {
        public string Username { get; set; }
        public string Password  { get; set; }
        public string Email { get; set; }
        public int Id { get; set; }

    }

    public class Credentials
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}

