using BlazorShop.Services;
using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public IEnumerable<ProductDTO> Products { get; set; }

        public string ErrorMessage { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await ClearLocalStorage();

                Products = await ManageProductsLocalStorageService.GetCollection();

                var shoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                var totalQty = shoppingCartItems.Sum(i => i.Quantity);

                ShoppingCartService.RaiseEventOnShoppingCartChanged(totalQty);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;

            }
        }

        protected IOrderedEnumerable<IGrouping<int,ProductDTO>> GetGroupedProductByCategory()
        {
            return from product in Products group product by product.CategoryId into prodByCatGroup orderby prodByCatGroup.Key select prodByCatGroup;
        }

        protected string GetCategoryName(IGrouping<int,ProductDTO> groupedProductDTOs) 
        {
            return groupedProductDTOs.FirstOrDefault(pg => pg.CategoryId == groupedProductDTOs.Key).CategoryName;
        }

        private async Task ClearLocalStorage()
        {
            await ManageProductsLocalStorageService.RemoveCollection();
            await ManageCartItemsLocalStorageService.RemoveCollection();
        }
    }
}
