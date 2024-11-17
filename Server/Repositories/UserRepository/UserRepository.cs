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

        public async Task<User?> GetByIdAsync(int id)
        {

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                // Log ou gérer le cas de valeur nulle ici
                throw new KeyNotFoundException($"User with id {id} was not found.");
            }
            return user;
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> AddAsync(User user)
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

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);
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

        public async Task SaveRefreshTokenAsync(int userId, string refreshToken)
        {
            // Trouver l'utilisateur par son ID
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                throw new Exception("Utilisateur introuvable.");
            }

            // Mettre à jour le RefreshToken
            user.RefreshToken = refreshToken;

            // Sauvegarder les modifications
            await _context.SaveChangesAsync();
        }
    }
}