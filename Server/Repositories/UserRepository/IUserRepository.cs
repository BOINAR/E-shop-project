// Repositories/Interfaces/IUserRepository.cs

using Server.Models;
namespace Server.Repositories.UserRepository
{
    public interface IUserRepository

    {
        Task<User?> GetByIdAsync(int id); // Récupérer un utilisateur par son ID
        Task<User?> GetByEmailAsync(string email); // Récupérer un utilisateur par son email
        Task<User> AddAsync(User user); // Ajouter un nouvel utilisateur
        Task<User?> UpdateAsync(User user); // Mettre à jour un utilisateur existant
        Task<bool> DeleteAsync(int id); // Supprimer un utilisateur par son ID
    }

}
