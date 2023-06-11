using Blazored.LocalStorage;
using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;

namespace BlazorShop.Services
{
    public class ManageCartItemsLocalStorageService : IManageCartItemsLocalStorageService
    {
        private readonly ILocalStorageService localStorageService;
        private readonly IShoppingCartService shoppingCartService;

        const string key = "CartItemCollection";

        public ManageCartItemsLocalStorageService(ILocalStorageService localStorageService,
                                                  IShoppingCartService shoppingCartService)
        {
            this.localStorageService = localStorageService;
            this.shoppingCartService = shoppingCartService;
        }

        public async Task<List<CartItemDTO>> GetCollection()
        {
            return await this.localStorageService.GetItemAsync<List<CartItemDTO>>(key)
                    ?? await AddCollection();
        }

        public async Task RemoveCollection()
        {
            await this.localStorageService.RemoveItemAsync(key);
        }

        public async Task SaveCollection(List<CartItemDTO> cartItemDtos)
        {
            await this.localStorageService.SetItemAsync(key, cartItemDtos);
        }

        private async Task<List<CartItemDTO>> AddCollection()
        {
            var shoppingCartCollection = await this.shoppingCartService.GetItems(HardCoded.UserId);

            if (shoppingCartCollection != null)
            {
                await this.localStorageService.SetItemAsync(key, shoppingCartCollection);
            }

            return shoppingCartCollection;

        }

    }
}
