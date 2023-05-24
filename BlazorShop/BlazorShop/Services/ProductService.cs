using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using System.Net.Http.Json;

namespace BlazorShop.Services
{
    public class ProductService:IProductService
    {
        private readonly HttpClient httpClient;

        public ProductService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductDTO>> GetItems()
        {
            try
            {
                var products = await this.httpClient.GetFromJsonAsync<IEnumerable<ProductDTO>>("api/Product");
                return products;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
