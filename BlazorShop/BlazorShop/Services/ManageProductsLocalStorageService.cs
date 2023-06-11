using Blazored.LocalStorage;
using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;

namespace BlazorShop.Services
{
    public class ManageProductsLocalStorageService : IManageProductsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IProductService productService;

        private const string key = "ProductCollection";

        public ManageProductsLocalStorageService(ILocalStorageService localStorageService,
                                                 IProductService productService)
        {
            this.localStorageService = localStorageService;
            this.productService = productService;
        }

        public async Task<IEnumerable<ProductDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<IEnumerable<ProductDTO>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        private async Task<IEnumerable<ProductDTO>> AddCollection()
        {
            var productCollection = await this.productService.GetItems();

            if (productCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, productCollection);
            }

            return productCollection;

        }

    }
}
