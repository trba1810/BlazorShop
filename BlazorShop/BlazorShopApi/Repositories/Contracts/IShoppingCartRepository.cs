using BlazorShopApi.Entities;
using BlazorShopModels.DTOs;

namespace BlazorShopApi.Repositories.Contracts
{
    public interface IShoppingCartRepository
    {
        Task<CartItem> AddItem(CartItemToAddDTO cartItemToAddDTO);
        Task<CartItem> UpdateQuantity(int id, CartItemQtyUpdateDTO cartItemQtyUpdateDTO);
        Task<CartItem> DeleteItem(int id);
        Task<CartItem> GetItem(int id);
        Task<CartItem> GetItems(int userId);
    }
}
