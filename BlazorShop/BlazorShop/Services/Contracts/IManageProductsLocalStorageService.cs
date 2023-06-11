using BlazorShopModels.DTOs;

namespace BlazorShop.Services.Contracts
{
    public interface IManageProductsLocalStorageService
    {
        Task<IEnumerable<ProductDTO>> GetCollection();
        Task RemoveCollection();
    }
}
