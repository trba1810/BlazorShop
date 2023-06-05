using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        public List<CartItemDTO> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ShoppingCartService.GetItems(HardCoded.UserId);
                CalculateCartSummary();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            RemoveCartItem(id);

            CalculateCartSummary();
        }

        protected async Task UpdateQuantity_Click(int id, int quantity)
        {
            try
            {
                if (quantity > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDTO
                    {
                        CartItemId = id,
                        Quantity = quantity
                    };

                    var returnedUpdatedItemDto = await this.ShoppingCartService.UpdateQuantity(updateItemDto);
                    UpdateItemTotalPrice(returnedUpdatedItemDto);
                    CalculateCartSummary();

                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(x => x.Id == id);
                    if (item != null)
                    {
                        item.Quantity = 1;
                        item.TotalPrice = item.Price;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void UpdateItemTotalPrice(CartItemDTO cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);
            if (item != null)
            {
                item.TotalPrice += cartItemDto.Price * cartItemDto.Quantity;
            }
        }

        private void CalculateCartSummary()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(x => x.TotalPrice).ToString("C");
        }

        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(x => x.Quantity);
        }

        private CartItemDTO GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private void RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);
        }



    }
}
