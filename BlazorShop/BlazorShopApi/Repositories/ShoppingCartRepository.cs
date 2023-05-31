﻿using BlazorShopApi.Data;
using BlazorShopApi.Entities;
using BlazorShopApi.Repositories.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApi.Repositories
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly ShopOnlineDbContext shopOnlineDbContext;

        public ShoppingCartRepository(ShopOnlineDbContext shopOnlineDbContext)
        {
            this.shopOnlineDbContext = shopOnlineDbContext;
        }

        private async Task<bool> CartItemExists(int cartId, int productId)
        {
            return await this.shopOnlineDbContext.CartItems.AnyAsync(c => c.CartId == cartId && c.ProductId == productId);

        }

        public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            if (await CartItemExists(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId) == false)
            {
                var item = await (from product in this.shopOnlineDbContext.Products
                                  where product.Id == cartItemToAddDTO.ProductId
                                  select new CartItem
                                  {
                                      CartId = cartItemToAddDTO.CartId,
                                      ProductId = product.Id,
                                      Quantity = cartItemToAddDTO.Quantity,
                                  }).SingleOrDefaultAsync();

                if (item != null)
                {
                    var result = await this.shopOnlineDbContext.AddAsync(item);
                    await this.shopOnlineDbContext.SaveChangesAsync();
                    return result.Entity;
                }
            }


            return null;
        }

        public Task<CartItem> DeleteItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetItem(int id)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems on cart.Id equals cartItem.CartId
                          where cartItem.Id == id
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {
            return await (from cart in this.shopOnlineDbContext.Carts
                          join cartItem in this.shopOnlineDbContext.CartItems on cart.Id equals cartItem.CartId
                          where cart.UserId == userId
                          select new CartItem
                          {
                              Id = cartItem.Id,
                              ProductId = cartItem.ProductId,
                              Quantity = cartItem.Quantity,
                              CartId = cartItem.CartId,
                          }).ToListAsync();
        }

        public Task<CartItem> UpdateQuantity(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}