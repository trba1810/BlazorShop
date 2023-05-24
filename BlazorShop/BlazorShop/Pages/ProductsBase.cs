using BlazorShop.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace BlazorShop.Pages
{
    public class ProductsBase:ComponentBase
    {
        [Inject]
        public IProductService ProductService { get; set; }
    }
}
