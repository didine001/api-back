namespace MyProject.Controllers
{
    public record UpdateUserDto(string Username, string Email, Guid? RoleId);
}