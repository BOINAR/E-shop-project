using Server.Data;

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
            var tokens = _context?.RefreshTokens?.Where(rt => rt.UserId == userId);
            if (tokens == null) { throw new Exception("token is null"); }
            foreach (var token in tokens)
            {
                token.IsRevoked = true;
            }
            if (_context == null) { throw new Exception("context is null"); }
            await _context.SaveChangesAsync();
        }
    }
}

