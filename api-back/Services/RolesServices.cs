using api_back.Models;
using Newtonsoft.Json;

namespace api_back.Services
{
    public class RoleServices
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

        public Role GetById(long id)
        {
            if (id == 0)
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

        public Role CreateRole(Role role)
        {
            if (role == null)
            {
                throw new Exception("Role shouldn't be null");
            }
            List<Role> roles = LoadRoles();
            role.Id = roles.Max(u => u.Id) + 1;
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

        public Role UpdateRole(Role roleToUpdate)
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

        public Role DeleteRole(Role role)
        {
            if (role == null)
            {
                throw new Exception("Role shouldn't be null");
            }
            List<Role> roles = LoadRoles();
            var dbRoles = roles.Find(r => r.Name == role.Name);
            if (dbRoles == null)
            {
                throw new Exception("Role doesn't exists");
            }
            roles.Remove(dbRoles);
            string roleSerialized = JsonConvert.SerializeObject(roles);
            File.WriteAllText("Data/Roles.json", roleSerialized);
            return role;
        }
        public bool Exists(int id)
        {
            if(id == 0)
            {
                throw new Exception("id shouldn't be equals to 0 ");
            }
            List<Role> roles = LoadRoles();
            return roles.Any(r => r.Id == id);
        }
    }
}