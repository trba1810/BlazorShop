﻿using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Products = await ProductService.GetItems();
        }

        protected IOrderedEnumerable<IGrouping<int,ProductDTO>> GetGroupedProductByCategory()
        {
            return from product in Products group product by product.CategoryId into prodByCatGroup orderby prodByCatGroup.Key select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int,ProductDTO> groupedProductDTOs) 
        {
            return groupedProductDTOs.FirstOrDefault(pg => pg.CategoryId == groupedProductDTOs.Key).CategoryName;
        }
    }
}
