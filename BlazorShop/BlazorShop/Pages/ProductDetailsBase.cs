using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ProductDetailsBase:ComponentBase
    {
        [Parameter]
        public int Id { get; set; }

        [Inject]
        public IProductService ProductService { get; set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageProductsLocalStorageService ManageProductsLocalStorageService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }


        public ProductDTO Product { get; set; }

        public string ErrorMessage { get; set; }

        private List<CartItemDTO> ShoppingCartItems { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                Product = await GetProductById(Id);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task AddtoCart_Click(CartItemToAddDTO cartItemToAddDTO)
        {
            try
            {
                var cartItemDto = await ShoppingCartService.AddItem(cartItemToAddDTO);
                if (cartItemDto != null)
                {
                    ShoppingCartItems.Add(cartItemDto);
                    await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
                }

                NavigationManager.NavigateTo("/ShoppingCart");
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task<ProductDTO> GetProductById(int id)
        {
            var productDtos = await ManageProductsLocalStorageService.GetCollection();

            if (productDtos != null)
            {
                return productDtos.SingleOrDefault(p => p.Id == id);
            }
            return null;
        }
    }
}
