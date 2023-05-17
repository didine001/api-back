using api_back.Models;
using api_back.Queries;
using api_back.Services;
using MediatR;

namespace api_back.Roles.Handlers
{
    public class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery, List<Role>>
    {
        private readonly IRoleService _roleService;

        public GetAllRolesHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task<List<Role>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = _roleService.GetAll();
            return roles;
        }
    }
}
