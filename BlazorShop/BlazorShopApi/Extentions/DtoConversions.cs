using BlazorShopApi.Entities;
using BlazorShopModels.DTOs;
using System.Net.NetworkInformation;

namespace BlazorShopApi.Extentions
{
    public static class DtoConversions
    {
        public static IEnumerable<ProductDTO> ConvertToDto(this IEnumerable<Product> products,
                                                            IEnumerable<ProductCategory> productCategories)
        {
            return (from product in products
                    join productCategory in productCategories
                    on product.CategoryId equals productCategory.Id
                    select new ProductDTO
                    {
                        Id = product.Id,
                        Name = product.Name,
                        Description = product.Description,
                        ImageURL = product.ImageURL,
                        Price = product.Price,
                        Quantity = product.Quantity,
                        CategoryId = product.CategoryId,
                        CategoryName = productCategory.Name
                    }).ToList();

        }

        public static ProductDTO ConvertToDto(this Product product,
                                                            ProductCategory productCategory)
        {
            return new ProductDTO
            {
                Id = product.Id,
                Name = productCategory.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Quantity = product.Quantity,
                CategoryId = product.CategoryId,
                CategoryName = productCategory.Name
            };


        }

        public static IEnumerable<CartItemDTO> ConvertToDto(this IEnumerable<CartItem> cartItems, IEnumerable<Product> products)
        {
            return (from cartItem in cartItems
                    join product in products on cartItem.ProductId equals product.Id
                    select new CartItemDTO
                    {
                        Id = cartItem.Id,
                        ProductId = cartItem.ProductId,
                        ProductName = product.Name,
                        ProductDescription = product.Description,
                        ProductImageURL = product.ImageURL,
                        Price = product.Price,
                        CartId = cartItem.CartId,
                        Quantity = cartItem.Quantity,
                        TotalPrice = product.Price * cartItem.Quantity,
                    }).ToList();
        }

        public static CartItemDTO ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDTO
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                CartId = cartItem.CartId,
                Quantity = cartItem.Quantity,
                TotalPrice = product.Price * cartItem.Quantity,
            };
        }
    }
}

