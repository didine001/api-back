using Api.Models;
using api_back.Models;
using api_back.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private UserServices userServices;
        private RoleServices roleServices;
        public UserController()
        {
            this.userServices = new UserServices();
            this.roleServices = new RoleServices();

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUserById(long id)
        {
            try
            {
                var user = userServices.GetById(id);
                userServices.DeleteUser(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById(int id, [FromBody] UpdateUserDto data)
        {
            try
            {
                if (!roleServices.Exists(id))
                {
                    throw new Exception("id doesnt exists");
                }
                var userToUpdate = userServices.GetById(id);
                userToUpdate.Username = data.Username;
                userToUpdate.Email = data.Email;
                userToUpdate.RoleId = data.RoleId;
                userServices.UpdateUser(userToUpdate);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return Ok(userServices.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(long id)
        {
            try
            {
                var user = userServices.GetById(id);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }

        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            try
            {
                userServices.CreateUser(user);
                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
