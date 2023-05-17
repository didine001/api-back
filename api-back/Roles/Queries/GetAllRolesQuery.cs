using api_back.Roles.Models;
using MediatR;

namespace api_back.Roles.Queries
{
    public class GetAllRolesQuery : IRequest<List<Role>>
    {

    }

}
