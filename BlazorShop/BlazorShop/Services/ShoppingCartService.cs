using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;

namespace BlazorShop.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CartItemDTO>> GetItems(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
