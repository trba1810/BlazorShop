using BlazorShopApi.Data;
using BlazorShopApi.Entities;
using BlazorShopApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace BlazorShopApi.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ShopOnlineDbContext context;

        public ProductRepository(ShopOnlineDbContext context)
        {
            this.context = context;
        }
        public async Task<IEnumerable<ProductCategory>> GetCategories()
        {
            var categories = await context.ProductCategories.ToListAsync();
            return categories;
        }

        public Task<ProductCategory> GetCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await context.Products.ToListAsync();
            return products;
        }
    }
}
