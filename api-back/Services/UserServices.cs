using Api.Models;
using api_back.Models;
using Newtonsoft.Json;

namespace api_back.Services
{
    public class UserServices
    {
        private List<User> LoadUsers()
        {
            string data = System.IO.File.ReadAllText("Data/Users.json");
            List<User> users = JsonConvert.DeserializeObject<List<User>>(data);

            if (string.IsNullOrEmpty(data) || users == null)
            {
                return new List<User>();
            }
            return users;
        }
        public List<User> GetAll() 
        {
            List<User> users = LoadUsers();
            return users;
        }

        public User GetById(long id) 
        {
            if (id == 0)
            {
                throw new Exception(" The id is not valid"); 
            }

            List<User> users = LoadUsers();
            var user= users.Find(u => u.Id == id);
            if (user == null)
            {
                throw new Exception("User doesn't exist in");
            }
            return user;
        }
        public User CreateUser(User user)
        {

            if (user == null)
            {
                throw new Exception("User shouldn't be null");
            }
            List<User> users = LoadUsers();
            user.Id = users.Max(u => u.Id) + 1;
            var userAlreadyExists = users.Any(u => u.Username == user.Username || u.Email == user.Email);
            if (userAlreadyExists)
            {
                throw new Exception("User already exists in the database");
            }
            users.Add(user);
            string usersSerialized = JsonConvert.SerializeObject(users);
            File.WriteAllText("Data/Users.json", usersSerialized);
            return user;
        }

        public User UpdateUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User shouldn't be null");
            }
            List<User> users = LoadUsers();
            var dbUser = users.Find(u => u.Id == user.Id);
            if (dbUser == null)
            {
                throw new Exception("User doesn't exists");
            }
            users.Remove(dbUser);
            users.Add(user);
            string usersSerialized = JsonConvert.SerializeObject(users);
            File.WriteAllText("Data/Users.json", usersSerialized);
            return user;
        }

        public User DeleteUser(User user)
        {
            if (user == null)
            {
                throw new Exception("User shouldn't be null");
            }
            List<User> users = LoadUsers();
            var dbUser = users.Find(u => u.Id == user.Id);
            if (dbUser == null)
            {
                throw new Exception("User doesn't exists");
            }
            users.Remove(dbUser);
            string usersSerialized = JsonConvert.SerializeObject(users);
            File.WriteAllText("Data/Users.json", usersSerialized);
            return user;
        }

        public User AddRoleToUser(User user, Role role)
        {
            if (user == null || role == null)
            {
                throw new Exception("User or role shouldn't be null");
            }
            List<User> users = LoadUsers();
            var dbUser = users.Find(u => u.Id == user.Id);

            if (dbUser == null)
            {
                throw new Exception("User doesn't exists");
            }
            users.Add(dbUser);
            string usersSerialized = JsonConvert.SerializeObject(users);
            File.WriteAllText("Data/Users.json", usersSerialized);
            return user;
        }

    }
}
