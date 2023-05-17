using api_back.Roles.Models;
using api_back.Users.Data;

namespace api_back.Roles.Services
{
    public class RolesServicesDb : IRoleService
    {
        private readonly UserApiDbContext dbContext;
        public RolesServicesDb(UserApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<Role> GetAll()
        {
            List<Role> roles = dbContext.Roles.ToList();
            return roles;
        }

        public Role GetById(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception(" The id is not valid");
            }
            var role = dbContext.Roles.FirstOrDefault(r => r.Id == id);
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
            var roleAlreadyExists = dbContext.Roles.Any(r => r.Name == role.Name);
            if (roleAlreadyExists)
            {
                throw new Exception("Role already exists in the database");
            }
            dbContext.Roles.Add(role);
            dbContext.SaveChanges();
            return role;
        }

        public Role Update(Role roleToUpdate)
        {
            if (roleToUpdate == null)
            {
                throw new Exception("Role shouldn't be null");
            }
            var existingRole = dbContext.Roles.FirstOrDefault(r => r.Id == roleToUpdate.Id);
            if (existingRole == null)
            {
                throw new Exception("Role doesn't exist");
            }
            dbContext.Roles.Update(existingRole);
            dbContext.SaveChanges();
            return roleToUpdate;
        }

        public void Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("id shouldn't be 0");
            }
            var dbRoles = dbContext.Roles.FirstOrDefault(r => r.Id == id);
            if (dbRoles == null)
            {
                throw new Exception("Role doesn't exists");
            }
            dbContext.Roles.Remove(dbRoles);
            dbContext.SaveChanges();
        }
        public bool Exists(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("id shouldn't be equals to 0 ");
            }
            return dbContext.Roles.Any(r => r.Id == id);
        }
    }
}
