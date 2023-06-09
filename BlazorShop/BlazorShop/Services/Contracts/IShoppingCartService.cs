﻿using BlazorShopModels.DTOs;

namespace BlazorShop.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<List<CartItemDTO>> GetItems(int userId);
        Task<CartItemDTO> AddItem(CartItemToAddDTO cartItemToAddDTO);
        Task<CartItemDTO> DeleteItem(int id);
        Task<CartItemDTO> UpdateQuantity(CartItemQtyUpdateDTO cartItemQtyUpdateDto);

        event Action<int> OnShoppingCartChanged;
        void RaiseEventOnShoppingCartChanged(int totalQty);

    }
}
