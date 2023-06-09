﻿using BlazorShopApi.Data;
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

        public async Task<ProductCategory> GetCategory(int id)
        {
            var category = await context.ProductCategories.SingleOrDefaultAsync(c => c.Id == id);
            return category;
        }

        public async Task<Product> GetItem(int id)
        {
            var product = await context.Products.Include(p => p.ProductCategory).SingleOrDefaultAsync(p => p.Id == id);
            return product;
        }

        public async Task<IEnumerable<Product>> GetItems()
        {
            var products = await context.Products.Include(p => p.ProductCategory).ToArrayAsync();
            return products;
        }

        public async Task<IEnumerable<Product>> GetItemsByCategory(int id)
        {
            var products = await context.Products.Include(p => p.ProductCategory).Where(p => p.CategoryId == id).ToListAsync();
            return products;
        }
    }
}
