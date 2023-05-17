using api_back.Users.Models;

namespace api_back.Users.Services
{
    public interface IUserService
    {
        List<User> GetAll();
        User GetById(Guid id);
        User Create(User user);
        User Update(User user);
        void Delete(Guid id);
    }
}
