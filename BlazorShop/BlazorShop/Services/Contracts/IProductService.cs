using BlazorShopModels.DTOs;

namespace BlazorShop.Services.Contracts
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetItems();
    }
}
