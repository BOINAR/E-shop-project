using Server.Models;
using Microsoft.EntityFrameworkCore;
using Server.Repositories.UserRepository;

namespace Server.Services.UserService
{

    // Services/UserService.cs
    using System;
    using System.Threading.Tasks;
    using Server.Models;
    using Server.Repositories.UserRepository;

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> RegisterAsync(User newUser)
        {
            // Vérification basique si un utilisateur avec cet email existe déjà
            if (newUser.Email == null)
            {
                throw new Exception("Email incorrect ou inexistant");
            }
            var existingUser = await _userRepository.GetByEmailAsync(newUser.Email);
            if (existingUser != null)
            {
                throw new Exception("Un utilisateur avec cet email existe déjà.");
            }

            // Hasher le mot de passe avant d'enregistrer l'utilisateur
            if (newUser.Password == null)
            {
                throw new Exception("Mot de passe incorrect ou inexistant");
            }
            newUser.Password = HashPassword(newUser.Password);
            await _userRepository.AddAsync(newUser);

            return newUser;
        }

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null || !VerifyPassword(password, user.Password))
            {
                throw new Exception("Email ou mot de passe incorrect.");
            }

            // Logique supplémentaire pour marquer l'utilisateur comme connecté si nécessaire
            return user;
        }

        public async Task LogoutAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Utilisateur non trouvé.");
            }

            // Logique supplémentaire pour marquer l'utilisateur comme déconnecté si nécessaire
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        private string HashPassword(string password)
        {
            // Logique pour hasher le mot de passe
            // Ici, tu pourrais utiliser un service de hachage comme BCrypt
            return password; // Remplace cette ligne par l'algorithme de hachage
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            // Logique pour vérifier si le mot de passe entré correspond au hash
            return enteredPassword == storedPasswordHash; // Remplace cette ligne par la vérification du hash
        }
    }
}