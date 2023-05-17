using api_back.Models;
using Newtonsoft.Json;

namespace api_back.Roles.Services
{
    public class RoleServices : IRoleService
    {
        private List<Role> LoadRoles()
        {
            string data = File.ReadAllText("Data/Roles.json");
            List<Role> roles = JsonConvert.DeserializeObject<List<Role>>(data);

            if (string.IsNullOrEmpty(data) || roles == null)
            {
                return new List<Role>();
            }
            return roles;
        }

        public List<Role> GetAll()
        {
            List<Role> roles = LoadRoles();
            return roles;
        }

        public Role GetById(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception(" The id is not valid");
            }

            List<Role> roles = LoadRoles();
            var role = roles.Find(r => r.Id == id);
            if (role == null)
            {
                throw new Exception("Role doesn't exist in");
            }
            return role;
        }

        public Role Create(Role role)
        {
            if (role == null)
            {
                throw new Exception("Role shouldn't be null");
            }
            List<Role> roles = LoadRoles();
            var roleAlreadyExists = roles.Any(r => r.Name == role.Name);
            if (roleAlreadyExists)
            {
                throw new Exception("Role already exists in the database");
            }
            roles.Add(role);
            string usersSerialized = JsonConvert.SerializeObject(roles);
            File.WriteAllText("Data/Roles.json", usersSerialized);
            return role;
        }

        public Role Update(Role roleToUpdate)
        {
            if (roleToUpdate == null)
            {
                throw new Exception("Role shouldn't be null");
            }
            List<Role> roles = LoadRoles();
            var existingRole = roles.Find(r => r.Id == roleToUpdate.Id);
            if (existingRole == null)
            {
                throw new Exception("Role doesn't exist");
            }
            roles.Remove(existingRole);
            roles.Add(roleToUpdate);
            string roleSerialized = JsonConvert.SerializeObject(roles);
            File.WriteAllText("Data/Roles.json", roleSerialized);
            return roleToUpdate;
        }

        public void Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("id shouldn't be 0");
            }
            List<Role> roles = LoadRoles();
            var dbRoles = roles.Find(r => r.Id == id);
            if (dbRoles == null)
            {
                throw new Exception("Role doesn't exists");
            }
            roles.Remove(dbRoles);
            string roleSerialized = JsonConvert.SerializeObject(roles);
            File.WriteAllText("Data/Roles.json", roleSerialized);
        }
        public bool Exists(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("id shouldn't be equals to 0 ");
            }
            List<Role> roles = LoadRoles();
            return roles.Any(r => r.Id == id);
        }
    }
}