using api_back.Users.Data;
using api_back.Users.Models;

namespace api_back.Users.Services
{
    public class UserServicesDb : IUserService
    {
        private readonly UserApiDbContext dbContext;
        public UserServicesDb(UserApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public List<User> GetAll()
        {
            List<User> users = dbContext.Users.ToList();
            return users;
        }

        public User GetById(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception(" The id is not valid");
            }
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User doesn't exist in");
            }
            return user;
        }
        public User Create(User user)
        {

            if (user == null)
            {
                throw new Exception("User shouldn't be null");
            }

            var userAlreadyExists = dbContext.Users.Any(u => u.Username == user.Username || u.Email == user.Email);
            if (userAlreadyExists)
            {
                throw new Exception("User already exists in the database");
            }
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }

        public User Update(User user)
        {
            if (user == null)
            {
                throw new Exception("User shouldn't be null");
            }
            var dbUser = dbContext.Users.FirstOrDefault(u => u.Id == user.Id);
            if (dbUser == null)
            {
                throw new Exception("User doesn't exists");
            }
            dbContext.Users.Update(user);
            dbContext.SaveChanges();
            return user;
        }

        public void Delete(Guid id)
        {
            if (id == null || id == Guid.Empty)
            {
                throw new Exception("User shouldn't be null");
            }
            var dbUser = dbContext.Users.First(u => u.Id == id);
            if (dbUser == null)
            {
                throw new Exception("User doesn't exists");
            }
            dbContext.Users.Remove(dbUser);
            dbContext.SaveChanges();
        }
    }
}
