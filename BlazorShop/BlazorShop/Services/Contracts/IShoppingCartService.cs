using BlazorShopModels.DTOs;

namespace BlazorShop.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDTO>> GetItems(int userId);
        Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO);
        Task<CartItemDTO> DeleteItem(int id);

    }
}
