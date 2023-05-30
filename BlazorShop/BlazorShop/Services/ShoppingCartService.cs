using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using System.Net.Http.Json;

namespace BlazorShop.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly HttpClient httpClient;

        public ShoppingCartService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                var response = await httpClient.PostAsJsonAsync<CartItemToAddDTO>("/api/ShoppingCart", cartItemToAddDTO);
                if(response.IsSuccessStatusCode)
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.NoContent) 
                    {
                        return default(CartItemDTO);
                    }
                    return await response.Content.ReadFromJsonAsync<CartItemDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception(message);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Task<IEnumerable<CartItemDTO>> GetItems(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
