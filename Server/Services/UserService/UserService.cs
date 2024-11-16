using Server.Models;
using Server.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;
using Server.Services.RefreshTokenServices;

namespace Server.Services.UserService
{


    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly RefreshTokenService _refreshTokenService;
        

        public UserService(RefreshTokenService refreshTokenService, IUserRepository userRepository)
        {
          _refreshTokenService = refreshTokenService;
            _userRepository = userRepository;
        }

        public async Task<User?> RegisterAsync(User newUser, string password)
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
            if (newUser.PasswordHash == null)
            {
                throw new Exception("Mot de passe incorrect ou inexistant");
            }
            newUser.PasswordHash = _passwordHasher.HashPassword(newUser, password);
            await _userRepository.AddAsync(newUser);

            return newUser;
        }

        public async Task<User?> LoginAsync(string email, string password)

        {

            var user = await _userRepository.GetByEmailAsync(email);
            if (user?.PasswordHash == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null; // Mot de passe incorrect
            }

            return user; // Authentification réussie
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task LogoutAsync(int id)
        {
            var user = await GetUserByIdAsync(id);

            if (user == null)
            {
                throw new Exception("Utilisateur non trouvé.");
            }

            // Révoquer tous les refresh tokens de l'utilisateur
            await _refreshTokenService.RevokeAllRefreshTokensAsync(id);

            // loguer l'événement de déconnexion
            Console.WriteLine($"Utilisateur {user.Email} déconnecté.");

        }

    }
}