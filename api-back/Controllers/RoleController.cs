using api_back.Models;
using api_back.Services;
using Microsoft.AspNetCore.Mvc;

namespace MyProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleService rolesServicesDb;


        public RoleController(IRoleService service)
        {
            this.rolesServicesDb = service;

        }

        [HttpGet]
        public IActionResult GetAllRoles()
        {
            return Ok(rolesServicesDb.GetAll());
        }


        [HttpGet("{id}")]
        public IActionResult GetRoleById(Guid id)
        {
            try
            {
                var role = rolesServicesDb.GetById(id);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateRole([FromBody] CreateRoleDto role)
        {
            try
            {
                Role roleToCreate = new Role(role.Name);
                rolesServicesDb.Create(roleToCreate);
                return Ok(new { Success = true });
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateRoleById(Guid id, [FromBody] UpdateRoleDto data)
        {
            try
            {
                var roleToUpdate = rolesServicesDb.GetById(id);
                roleToUpdate.UpdateRole(data.name);
                rolesServicesDb.Update(roleToUpdate);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRoleById(Guid id)
        {
            try
            {
                rolesServicesDb.Delete(id);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
            return Ok(new { Success = true });
        }
    }
}
