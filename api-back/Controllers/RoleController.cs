using api_back.Models;
using api_back.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private RoleServices roleServices;

        public RoleController()
        {
            this.roleServices = new RoleServices();
        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(roleServices.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetRoleById(long id)
        {
            try
            {
                var role = roleServices.GetById(id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] Role role)
        {
            try
            {
                roleServices.CreateRole(role);
                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoleById(long id, [FromBody] Role data)
        {
            try
            {
                var roleToUpdate = roleServices.GetById(id);
                roleToUpdate.Name = data.Name;
                roleServices.UpdateRole(roleToUpdate);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(long id)
        {
            try
            {
                var role = roleServices.GetById(id);
                roleServices.DeleteRole(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }
    }
}
