using api_back.Roles.Services;
using api_back.Users.Models;
using api_back.Users.Services;
using Microsoft.AspNetCore.Mvc;

namespace api_back.Users.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userServices;
        private IRoleService roleServices;

        public UserController(IUserService service, IRoleService roleService)
        {

            userServices = service;
            roleServices = roleService;

        }

        [HttpDelete("{id}")]

        public IActionResult DeleteUserById(Guid id)
        {
            try
            {
                var user = userServices.GetById(id);
                userServices.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserById(Guid id, [FromBody] UpdateUserDto data)
        {

            try
            {
                if (!roleServices.Exists(id))
                {
                    throw new Exception("id doesnt exists");
                }

                var userToUpdate = userServices.GetById(id);
                userToUpdate.UpdateUser(data.Username, data.Email, data.RoleId);
                userServices.Update(userToUpdate);
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
        public IActionResult GetUserById(Guid id)
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
        public IActionResult CreateUser([FromBody] CreateUserDto user)
        {
            try
            {
                User userToCreate = new User(user.Username, user.Email, user.Password);
                userServices.Create(userToCreate);
                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
