﻿using BlazorShop.Services.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorShop.Pages
{
    public class ShoppingCartBase : ComponentBase
    {
        [Inject]
        public IJSRuntime Js { get;set; }

        [Inject]
        public IShoppingCartService ShoppingCartService { get; set; }

        [Inject]
        public IManageCartItemsLocalStorageService ManageCartItemsLocalStorageService { get; set; }

        public List<CartItemDTO> ShoppingCartItems { get; set; }
        public string ErrorMessage { get; private set; }

        protected string TotalPrice { get; set; }
        protected int TotalQuantity { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                ShoppingCartItems = await ManageCartItemsLocalStorageService.GetCollection();
                CartChanged();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message;
            }
        }

        protected async Task DeleteCartItem_Click(int id)
        {
            var cartItemDto = await ShoppingCartService.DeleteItem(id);

            await RemoveCartItem(id);

            CartChanged();
        }

        protected async Task UpdateQtyCartItem_Click(int id, int qty)
        {
            try
            {
                if (qty > 0)
                {
                    var updateItemDto = new CartItemQtyUpdateDTO
                    {
                        CartItemId = id,
                        Quantity = qty
                    };

                    var returnedUpdateItemDto = await this.ShoppingCartService.UpdateQuantity(updateItemDto);

                    await UpdateItemTotalPrice(returnedUpdateItemDto);

                    CartChanged();

                    await MakeUpdateQtyButtonVisible(id, false);


                }
                else
                {
                    var item = this.ShoppingCartItems.FirstOrDefault(i => i.Id == id);

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

        protected async Task UpdateQty_Input(int id)
        {
            await MakeUpdateQtyButtonVisible(id, true);
        }

        private async Task MakeUpdateQtyButtonVisible(int id, bool visible)
        {
            await Js.InvokeVoidAsync("MakeUpdateQtyButtonVisible", id, visible);
        }

        private async Task UpdateItemTotalPrice(CartItemDTO cartItemDto)
        {
            var item = GetCartItem(cartItemDto.Id);

            if (item != null)
            {
                item.TotalPrice = cartItemDto.Price * cartItemDto.Quantity;
            }

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);

        }
        private void CalculateCartSummaryTotals()
        {
            SetTotalPrice();
            SetTotalQuantity();
        }

        private void SetTotalPrice()
        {
            TotalPrice = this.ShoppingCartItems.Sum(p => p.TotalPrice).ToString("C");
        }
        private void SetTotalQuantity()
        {
            TotalQuantity = this.ShoppingCartItems.Sum(p => p.Quantity);
        }


        private CartItemDTO GetCartItem(int id)
        {
            return ShoppingCartItems.FirstOrDefault(x => x.Id == id);
        }

        private async Task RemoveCartItem(int id)
        {
            var cartItemDto = GetCartItem(id);

            ShoppingCartItems.Remove(cartItemDto);

            await ManageCartItemsLocalStorageService.SaveCollection(ShoppingCartItems);
        }

        private void CartChanged()
        {
            CalculateCartSummaryTotals();
            ShoppingCartService.RaiseEventOnShoppingCartChanged(TotalQuantity);
        }



    }
}
