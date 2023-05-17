using System.ComponentModel.DataAnnotations;

namespace api_back.Users.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public string Email { get; private set; }
        public Guid? RoleId { get; private set; }


        public User(string username, string email, string password)
        {

            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            Password = password;
        }
        public void UpdateUser(string username, string email, Guid? roleId)
        {
            Username = username;
            Email = email;
            RoleId = roleId;
        }

    }

    public class AuthenticationResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
    }
}
