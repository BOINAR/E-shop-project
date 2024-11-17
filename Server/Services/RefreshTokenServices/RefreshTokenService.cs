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

        public async Task RevokeAllRefreshTokensAsync(int userId)
        {
            // Récupérer un seul token (le refresh token) pour cet utilisateur
            var token = await _context.RefreshTokens
                                      .Where(rt => rt.UserId == userId)
                                      .FirstOrDefaultAsync(); // Récupère le premier ou aucun token

            if (token == null)
            {
                throw new Exception("Aucun token trouvé pour cet utilisateur.");
            }

            // Marquer le token comme révoqué
            token.IsRevoked = true;

            // Sauvegarder les modifications dans la base de données
            await _context.SaveChangesAsync();
        }
    }
}

