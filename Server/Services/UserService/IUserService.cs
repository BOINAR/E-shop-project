using Server.Models;

namespace Server.Services.UserService
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(User newUser, string password); // Méthode asynchrone pour l'enregistrement
        Task<User?> LoginAsync(string email, string password); // Méthode asynchrone pour la connexion
        Task LogoutAsync(int userId); // Méthode synchronisée pour la déconnexion
        Task<User?> GetUserById(int userId);
        Task SaveRefreshToken(int userId, string refreshToken);
        Task<User?> UpdateUser(int userId, User updateUser);
        Task<User?> GetUserByRefreshToken(string refreshToken);
    }
}