using BlazorShopApi.Data;
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

        private async Task<bool> CartItemExists(int cartId,int productId)
        {
            return await this.shopOnlineDbContext.CartItems.AnyAsync(c=>c.CartId == cartId && c.ProductId == productId);

        }

        public async Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            if(await CartItemExists(cartItemToAddDTO.CartId, cartItemToAddDTO.ProductId)== false)
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

        public Task<CartItem> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> GetItems(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<CartItem> UpdateQuantity(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDTO)
        {
            throw new NotImplementedException();
        }
    }
}
