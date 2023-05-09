namespace MyProject.Controllers
{
    public class UpdateUserDto
    {
        public string Username { get; set; }
        public string Email { get;set; }
        public int? RoleId { get; set; } 
    }
}