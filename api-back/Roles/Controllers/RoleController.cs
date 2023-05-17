using api_back.Queries;
using api_back.Roles.Models;
using api_back.Roles.Services;
using api_back.Users.Controllers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace api_back.Roles.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private IRoleService rolesServicesDb;
        private readonly IMediator _mediator;

        public RoleController(IRoleService service, IMediator mediator)
        {
            rolesServicesDb = service;
            _mediator = mediator;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            var query = new GetAllRolesQuery();
            var result = await _mediator.Send(query);
            return Ok(result);

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
