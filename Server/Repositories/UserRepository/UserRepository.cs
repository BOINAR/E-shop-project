// Repositories/UserRepository.cs
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Models;


namespace Server.Repositories.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly EcommerceDbContext _context; //  contexte de base de données

        public UserRepository(EcommerceDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetUserByIdAsync(int userId)
        {

            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                // Log ou gérer le cas de valeur nulle ici
                throw new KeyNotFoundException($"User with id {userId} was not found.");
            }
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(int userId)
        {
            var user = await GetUserByIdAsync(userId);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _context.RefreshTokens
                .Where(rt => rt.Token == refreshToken && !rt.Revoked.HasValue && rt.Expires > DateTime.UtcNow)
                .Select(rt => rt.User) // Navigation vers l'utilisateur associé
                .FirstOrDefaultAsync();
        }

        public async Task SaveRefreshTokenAsync(RefreshToken refreshToken)
        {
            try
            {
                // Met à jour l'utilisateur dans la base de données
                await _context.RefreshTokens.AddAsync(refreshToken);

                // Sauvegarde les changements dans la base de données
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // Gérer les erreurs ici
                // Log l'erreur et/ou lance une exception pour signaler qu'il y a eu un problème lors de la sauvegarde
                // Par exemple, tu pourrais loguer l'erreur avec un logger ou gérer plus finement l'exception
                throw new Exception("Erreur lors de la sauvegarde du refresh token.", ex);
            }

        }
    }
}