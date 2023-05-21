using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorShopModels.DTOs
{
    public class CartItemQtyUpdateDTO
    {
        public int CartItemId { get; set; }
        public int Quantity { get; set; }
    }
}
