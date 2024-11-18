using Server.Data;
using Microsoft.EntityFrameworkCore;

namespace Server.Services.RefreshTokenServices
{
    public class RefreshTokenService
    {
        private readonly EcommerceDbContext _context;

        public RefreshTokenService(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task RevokeRefreshToken(int userId)
        {
            // Récupérer le refreshtoken pour cet utilisateur
            var token = await _context.RefreshTokens
                                      .Where(rt => rt.UserId == userId)
                                      .FirstOrDefaultAsync(); // Récupère le token

            if (token == null)
            {
                throw new Exception("Token introuvable.");
            }

            token.Revoked = DateTime.UtcNow; // Met à jour la propriété Revoked

            // Sauvegarde des modifications
            await _context.SaveChangesAsync();
        }
    }
}

