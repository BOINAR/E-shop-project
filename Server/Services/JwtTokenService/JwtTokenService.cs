using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Server.Models;



namespace Server.Services.JwtTokenService
{

    public class JwtTokenService
    {
        private readonly IConfiguration _configuration;

        public JwtTokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateAccessToken(User user)
        {
            var jwtSettings = _configuration.GetSection("JwtSettings");

            // Vérifier la présence des clés essentielles dans la configuration
            if (string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JWT_SECRET_KEY")) ||
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JWT_ISSUER")) ||
                string.IsNullOrEmpty(Environment.GetEnvironmentVariable("JWT_AUDIENCE")))
            {
                throw new InvalidOperationException("Configuration JWT invalide. Vérifiez les clés.");
            }

            var secretKeyString = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")
        ?? throw new InvalidOperationException("SecretKey manquant dans la configuration.");

            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKeyString));

            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString() ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(ClaimTypes.Name, user.UserName ?? string.Empty)
            // Ajouter d'autres claims si nécessaire
        };
            // Gérer l'expiration du token
            double expirationMinutes;
            if (!double.TryParse(jwtSettings["AccessTokenExpirationMinutes"], out expirationMinutes))
            {
                expirationMinutes = 60;
            }

            var token = new JwtSecurityToken(
                issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
                audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
                claims: claims,
                expires: DateTime.Now.AddMinutes(expirationMinutes),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            // Générer un token de rafraîchissement sous forme de base64
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
        }
    }
}

