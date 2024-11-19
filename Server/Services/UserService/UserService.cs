using Server.Models;
using Server.Repositories.UserRepository;
using Microsoft.AspNetCore.Identity;
using Server.Services.RefreshTokenServices;
using Server.Services.JwtTokenService;

namespace Server.Services.UserService
{


    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher = new PasswordHasher<User>();
        private readonly RefreshTokenService _refreshTokenService;
        private readonly JWTtokenService _JwtTokenService;

        private readonly ILogger<AuthController> _logger;


        public UserService(RefreshTokenService refreshTokenService, IUserRepository userRepository, ILogger<AuthController> logger, JWTtokenService JwtTokenService)
        {
            _JwtTokenService = JwtTokenService;
            _refreshTokenService = refreshTokenService;
            _userRepository = userRepository;
            _logger = logger;
        }

        // Méthode d'enregistrement de l'utilisateur
        public async Task<User?> RegisterAsync(User newUser, string password)
        {
            // Vérification basique si un utilisateur avec cet email existe déjà
            if (string.IsNullOrEmpty(newUser.Email))
            {
                throw new Exception("Email incorrect ou inexistant");
            }

            var existingUser = await _userRepository.GetUserByEmailAsync(newUser.Email);
            if (existingUser != null)
            {
                throw new Exception("Un utilisateur avec cet email existe déjà.");
            }

            // Hasher le mot de passe avant d'enregistrer l'utilisateur
            if (string.IsNullOrEmpty(password))
            {
                throw new Exception("Mot de passe incorrect ou inexistant");
            }

            newUser.Password = _passwordHasher.HashPassword(newUser, password);

            // Enregistrer l'utilisateur dans la base de données
            var savedUser = await _userRepository.AddUserAsync(newUser);

            // Générer un nouveau refresh token
            var refreshToken = _JwtTokenService.GenerateRefreshToken();

            // Enregistrer le refresh token dans la base de données
            await SaveRefreshToken(savedUser, refreshToken);

            return savedUser;
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

            // Loguer la tentative de déconnexion
            _logger.LogInformation($"Tentative de déconnexion pour l'utilisateur avec l'ID {userId}.");

            var user = await GetUserById(userId);

            if (user == null)
            {
                // Loguer si l'utilisateur n'est pas trouvé
                _logger.LogWarning($"Aucun utilisateur trouvé avec l'ID {userId}.");
                throw new Exception("Utilisateur non trouvé.");
            }

            // Loguer la déconnexion de l'utilisateur
            _logger.LogInformation($"Utilisateur {user.Email} déconnecté.");

            // Révoquer les refresh tokens
            await _refreshTokenService.RevokeRefreshToken(userId);

        }

        public async Task SaveRefreshToken(User user, string refreshToken)
        {
            try
            {
                // Crée un nouvel objet RefreshToken pour la table RefreshTokens
                var refreshTokenEntity = new RefreshToken
                {
                    UserId = user.Id, // Associe le refreshToken à l'utilisateur
                    Token = refreshToken,
                    Expires = DateTime.UtcNow.AddDays(7),  // Par exemple, expiration dans 7 jours
                    Created = DateTime.UtcNow,
                };

                // Ajoute le refreshToken à la table RefreshTokens
                await _userRepository.SaveRefreshTokenAsync(refreshTokenEntity);

                // Sauvegarde les changements dans la base de donnée
            }
            catch (Exception ex)
            {
                // Gérer les erreurs ici
                throw new Exception("Erreur lors de la sauvegarde du refresh token.", ex);
            }
        }

        public async Task<User?> ValidateRefreshTokenAsync(string refreshToken)
        {
            var user = await _userRepository.GetUserByRefreshTokenAsync(refreshToken);
            if (user == null)
                return null;

            return user;
        }



        // Méthode pour mettre à jour un utilisateur
        public async Task<User?> UpdateUser(int userId, User updateUser)
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
            return await _userRepository.UpdateUserAsync(user);
        }


    }
}