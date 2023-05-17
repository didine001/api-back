namespace api_back.Users.Controllers
{
    public record CreateUserDto(string Username, string Email, string Password);
}