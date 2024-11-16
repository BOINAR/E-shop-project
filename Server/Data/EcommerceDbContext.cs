using Microsoft.EntityFrameworkCore;
using Server.Models; // Ajuste selon le namespace des modèles

namespace Server.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> options) : base(options) { }

        // Définis ici les DbSets pour chaque entité
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<Order> Orders { get; set; } = null!;
        public DbSet<OrderItem> OrderItems { get; set; } = null!;
        public DbSet<Category> Categories { get; set; } = null!;
        public DbSet<Cart> Carts { get; set; } = null!;
        public DbSet<CartItem> CartItems { get; set; } = null!;
        public DbSet<Address> Addresses { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;


    }
}