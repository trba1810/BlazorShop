using BlazorShopApi.Entities;

namespace BlazorShopApi.Repositories.Contracts
{
    public interface IContractRepository
    {
        Task<IEnumerable<Product>> GetItems();
        Task<IEnumerable<ProductCategory>> GetCategories();
        Task<Product> GetItem(int id);
        Task<ProductCategory> GetCategory(int id);

    }
}
