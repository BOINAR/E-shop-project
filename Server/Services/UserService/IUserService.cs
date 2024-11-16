using Server.Models;

namespace Server.Services.UserService
{
    public interface IUserService
    {
        Task<User?> RegisterAsync(User newUser, string password); // Méthode asynchrone pour l'enregistrement
        Task<User?> LoginAsync(string email, string password); // Méthode asynchrone pour la connexion
        Task LogoutAsync(int userId); // Méthode synchronisée pour la déconnexion
        Task<User?> GetUserByIdAsync(int id);
    }
}