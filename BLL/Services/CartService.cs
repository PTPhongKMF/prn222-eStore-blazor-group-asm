using AutoMapper;
using BLL.DTOs;
using DAL.Entities;
using DAL.Repositories;

namespace BLL.Services
{
    public class CartService
    {
        private readonly CartRepository cartRepository;
        private readonly IMapper mapper;

        public CartService(CartRepository cartRepository, IMapper mapper)
        {
            this.cartRepository = cartRepository;
            this.mapper = mapper;
        }

        public async Task<List<CartDTO>> GetCartByMemberIdAsync(int memberId)
        {
            var cartItems = await cartRepository.GetCartByMemberIdAsync(memberId);
            return cartItems.Select(c => mapper.Map<CartDTO>(c)).ToList();
        }

        // Add item to cart or update quantity if exists
        public async Task<CartDTO> AddToCartAsync(CartDTO cartDto)
        {
            var cartEntity = mapper.Map<Cart>(cartDto);
            var addedCart = await cartRepository.AddToCartAsync(cartEntity);
            return mapper.Map<CartDTO>(addedCart);
        }
        public async Task<CartDTO> AddToCartAsync(int memberId, int productId, int quantity)
        {
            var cartDto = new CartDTO
            {
                MemberId = memberId,
                ProductId = productId,
                Quantity = quantity
            };
            return await AddToCartAsync(cartDto);
        }

        // Update cart item quantity
        public async Task<CartDTO?> UpdateCartItemQuantityAsync(int memberId, int productId, int newQuantity)
        {
            var updatedCart = await cartRepository.UpdateCartItemQuantityAsync(memberId, productId, newQuantity);
            return updatedCart != null ? mapper.Map<CartDTO>(updatedCart) : null;
        }

        // Remove item from cart
        public async Task<bool> RemoveFromCartAsync(int memberId, int productId)
        {
            return await cartRepository.RemoveFromCartAsync(memberId, productId);
        }

        // Clear all cart items for a member
        public async Task<bool> ClearCartAsync(int memberId)
        {
            return await cartRepository.ClearCartAsync(memberId);
        }
    }
}
