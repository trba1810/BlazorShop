using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ShoppingCartBase:ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public IEnumerable<CartItemDTO> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }
        
    }
}
