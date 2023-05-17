using api_back.Roles.Models;

namespace api_back.Roles.Services
{
    public interface IRoleService
    {
        List<Role> GetAll();

        Role GetById(Guid id);

        Role Create(Role role);

        Role Update(Role role);

        void Delete(Guid id);

        bool Exists(Guid id);
    }
}
