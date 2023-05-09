using api_back.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Username { get; set; }
        public string? Password { get; set; }
        public string Email { get; set; }
        public string? Adress { get; set; }
        public int? RoleId { get; set; }

        }

        public class AuthenticationResult
        {
            public bool Success { get; set; }
            public string Message { get; set; }
        }
    }

