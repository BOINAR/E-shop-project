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
            // Rechercher un utilisateur avec le RefreshToken spécifié
            return await _context.Users
                .Where(u => u.RefreshToken == refreshToken)
                .FirstOrDefaultAsync();  // Utilise FirstOrDefaultAsync pour renvoyer un utilisateur ou null
        }

        public async Task SaveRefreshTokenAsync(User user)
        {
            _context.Users.Update(user);

            // Sauvegarder les modifications
            await _context.SaveChangesAsync();
        }
    }
}