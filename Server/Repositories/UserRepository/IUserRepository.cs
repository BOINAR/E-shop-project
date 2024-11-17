// Repositories/Interfaces/IUserRepository.cs

using Server.Models;
namespace Server.Repositories.UserRepository
{
    public interface IUserRepository

    {
        Task<User?> GetUserByIdAsync(int UserId); // Récupérer un utilisateur par son ID
        Task<User?> GetUserByEmailAsync(string email); // Récupérer un utilisateur par son email
        Task<User> AddUserAsync(User user); // Ajouter un nouvel utilisateur
        Task<User?> UpdateUserAsync(User user); // Mettre à jour un utilisateur existant
        Task<bool> DeleteUserAsync(int UserId); // Supprimer un utilisateur par son ID
    }

}
