using miso_greenshop_api.Domain.Interfaces.Repositories;
using miso_greenshop_api.Domain.Models;
using miso_greenshop_api.Infrastructure.Persistance;

namespace miso_greenshop_api.Infrastructure.Repositories
{
    public class CartItemsRepository(ApplicationDbContext dbContext) : 
        ICartItemsRepository
    {
        private readonly ApplicationDbContext _dbContext = 
            dbContext;

        public async Task<CartItem?> GetCartItemByIdsAsync(
            string cartId, 
            string plantId)
        {
            return await _dbContext.CartItems!
                .FindAsync(
                cartId, 
                plantId);
        }

        public async Task AddCartItem(CartItem cartItem)
        {
            _dbContext.CartItems!
                .Add(cartItem);
            await _dbContext
                .SaveChangesAsync();
        }

        public async Task UpdateCartItemQuantity(
            CartItem cartItem, 
            int quantity)
        {
            cartItem.Quantity = quantity;
            await _dbContext
                .SaveChangesAsync();
        }

        public async Task DeleteCartItemAsync(CartItem cartItem)
        {
            _dbContext.CartItems!
                .Remove(cartItem);
            await _dbContext
                .SaveChangesAsync();
        }

        public async Task DeleteCartItemsAsync(List<CartItem> cartItems)
        {
            _dbContext.CartItems!
                .RemoveRange(cartItems);
            await _dbContext
                .SaveChangesAsync();
        }
    }
}
