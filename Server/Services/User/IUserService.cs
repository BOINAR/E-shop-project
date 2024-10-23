using Server.Models;

namespace Server.Services
{
    public interface IUserService
    {
        User Register(User newUser);
        User Login(string email, string password);
        void Logout(int userId);

    }
}