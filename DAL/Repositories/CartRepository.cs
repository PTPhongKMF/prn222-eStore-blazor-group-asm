using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CartRepository
    {
        private readonly EStoreDatabaseContext dbContext;

        public CartRepository(EStoreDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Cart>> GetCartByMemberIdAsync(int memberId)
        {
            return await dbContext.Cart
                .Include(c => c.Product)
                    .ThenInclude(p => p.Category)
                .Include(c => c.Member)
                .Where(c => c.MemberId == memberId)
                .OrderBy(c => c.Product.ProductName)
                .ToListAsync();
        }

        public async Task<Cart> AddToCartAsync(Cart cartItem)
        {
            var existingItem = await dbContext.Cart
                .FirstOrDefaultAsync(c => c.MemberId == cartItem.MemberId && c.ProductId == cartItem.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += cartItem.Quantity;
                await dbContext.SaveChangesAsync();

                await dbContext.Entry(existingItem)
                    .Reference(c => c.Product)
                    .LoadAsync();
                await dbContext.Entry(existingItem.Product)
                    .Reference(p => p.Category)
                    .LoadAsync();
                await dbContext.Entry(existingItem)
                    .Reference(c => c.Member)
                    .LoadAsync();

                return existingItem;
            }
            else
            {
                dbContext.Cart.Add(cartItem);
                await dbContext.SaveChangesAsync();

                await dbContext.Entry(cartItem)
                    .Reference(c => c.Product)
                    .LoadAsync();
                await dbContext.Entry(cartItem.Product)
                    .Reference(p => p.Category)
                    .LoadAsync();
                await dbContext.Entry(cartItem)
                    .Reference(c => c.Member)
                    .LoadAsync();

                return cartItem;
            }
        }

        public async Task<Cart?> UpdateCartItemQuantityAsync(int memberId, int productId, int newQuantity)
        {
            var cartItem = await dbContext.Cart
                .FirstOrDefaultAsync(c => c.MemberId == memberId && c.ProductId == productId);

            if (cartItem == null)
                return null;

            if (newQuantity <= 0)
            {
                dbContext.Cart.Remove(cartItem);
                await dbContext.SaveChangesAsync();
                return null;
            }

            cartItem.Quantity = newQuantity;
            await dbContext.SaveChangesAsync();

            await dbContext.Entry(cartItem)
                .Reference(c => c.Product)
                .LoadAsync();
            await dbContext.Entry(cartItem.Product)
                .Reference(p => p.Category)
                .LoadAsync();
            await dbContext.Entry(cartItem)
                .Reference(c => c.Member)
                .LoadAsync();

            return cartItem;
        }

        public async Task<bool> RemoveFromCartAsync(int memberId, int productId)
        {
            var cartItem = await dbContext.Cart
                .FirstOrDefaultAsync(c => c.MemberId == memberId && c.ProductId == productId);

            if (cartItem == null)
                return false;

            dbContext.Cart.Remove(cartItem);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ClearCartAsync(int memberId)
        {
            var cartItems = await dbContext.Cart
                .Where(c => c.MemberId == memberId)
                .ToListAsync();

            if (!cartItems.Any())
                return false;

            dbContext.Cart.RemoveRange(cartItems);
            await dbContext.SaveChangesAsync();
            return true;
        }

    }
}