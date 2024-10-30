using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Server.Models;
using Server.Repositories;
using Server.Repositories.CartItemRepository;
using Server.Repositories.CartRepository;


namespace Server.Services.CartService
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public CartService(ICartRepository cartRepository, ICartItemRepository cartItemRepository)
        {
            _cartRepository = cartRepository;
            _cartItemRepository = cartItemRepository;
        }

        // Récupère le panier d'un utilisateur spécifique
        public async Task<Cart?> GetCartByUserIdAsync(int userId)
        {
            return await _cartRepository.GetCartByUserIdAsync(userId) 
                   ?? new Cart { Id = userId, CartItems = new List<CartItem>() };
        }

        // Ajoute un article au panier
        public async Task<bool> AddItemToCart(Cart cart, CartItem cartItem)
        {
            if (cart.CartItems == null) cart.CartItems = new List<CartItem>();

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItem.ProductId);
            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
            }
            else
            {
                cart.CartItems.Add(cartItem);
            }

            return await _cartRepository.UpdateCartAsync(cart);
        }

        // Met à jour un article dans le panier
        public async Task<bool> UpdateCartItem(int userId, int cartItemId, CartItem updatedCartItem)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null) throw new Exception("cart not found.");

            var existingItem = cart.CartItems?.FirstOrDefault(ci => ci.Id == cartItemId); 
            if (existingItem == null) { throw new Exception("CartItem not found"); } 
            existingItem.ProductId = updatedCartItem.ProductId; 
            existingItem.Quantity = updatedCartItem.Quantity; 
            existingItem.Price = updatedCartItem.Price; 
            return await _cartRepository.UpdateCartAsync(cart);
        }

        // Supprime un article du panier
        public async Task<bool> RemoveItemFromCart(int cartItemId)
        {
            var item = await _cartItemRepository.GetCartItemByIdAsync(cartItemId);
            if (item == null) throw new Exception("L'article n'existe pas dans le panier.");

            await _cartItemRepository.DeleteAsync(cartItemId);
            return true;
        }

        // Calcule le total du panier pour un utilisateur
        public async Task<decimal> CalculateCartTotal(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null || cart.CartItems == null) return 0;

            return cart.CartItems.Sum(ci => ci.Quantity * (ci.Product?.Price ?? 0));
        }
    }
}