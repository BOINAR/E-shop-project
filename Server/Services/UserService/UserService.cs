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
            var existingUser = await _userRepository.GetUserByEmailAsync(newUser.Email);
            if (existingUser != null)
            {
                throw new Exception("Un utilisateur avec cet email existe déjà.");
            }

            // Hasher le mot de passe avant d'enregistrer l'utilisateur
            if (newUser.Password == null)
            {
                throw new Exception("Mot de passe incorrect ou inexistant");
            }
            newUser.Password = _passwordHasher.HashPassword(newUser, password);
            await _userRepository.AddUserAsync(newUser);

            return newUser;
        }

        public async Task<User?> LoginAsync(string email, string password)

        {

            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user?.Password == null) return null;

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, password);
            if (result == PasswordVerificationResult.Failed)
            {
                return null; // Mot de passe incorrect
            }

            return user; // Authentification réussie
        }

        public async Task<User?> GetUserById(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }

        public async Task LogoutAsync(int userId)
        {
            var user = await GetUserById(userId);

            if (user == null)
            {
                throw new Exception("Utilisateur non trouvé.");
            }

            // Révoquer tous les refresh tokens de l'utilisateur
            await _refreshTokenService.RevokeAllRefreshTokensAsync(userId);

            // loguer l'événement de déconnexion
            Console.WriteLine($"Utilisateur {user.Email} déconnecté.");

        }

        public async Task SaveRefreshToken(int userId, string refreshToken)
        {
            var user = await GetUserById(userId);
            if (user == null)
            {
                throw new Exception($"Utilisateur avec l'ID {userId} introuvable.");
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7); // Par exemple, valide 7 jours

            await _userRepository.SaveRefreshTokenAsync(user);
        }
        // Méthode pour mettre à jour un utilisateur
        public async Task UpdateUserAsync(int userId, User updateUser)
        {
            // Récupérer l'utilisateur existant
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new Exception("Utilisateur introuvable.");
            }

            // Logique métier : Mise à jour des informations
            user.UserName = updateUser.UserName ?? user.UserName;
            user.Email = updateUser.Email ?? user.Email;
            user.FirstName = updateUser.FirstName ?? user.FirstName;
            user.LastName = updateUser.LastName ?? user.LastName;

            // Si un nouveau mot de passe est fourni, on le hache et on le met à jour
            if (!string.IsNullOrEmpty(updateUser.Password))
            {
                user.Password = _passwordHasher.HashPassword(user, updateUser.Password);  // Hachage du mot de passe
            }

            // Mettre à jour l'utilisateur dans la base de données via le repository
            await _userRepository.UpdateUserAsync(user);
        }

        public async Task<User?> GetUserByRefreshToken(string refreshToken)
        {

          return  await _userRepository.GetUserByRefreshTokenAsync(refreshToken);

        }

    }
}