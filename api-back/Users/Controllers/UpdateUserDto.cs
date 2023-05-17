namespace api_back.Users.Controllers
{
    public record UpdateUserDto(string Username, string Email, Guid? RoleId);
}