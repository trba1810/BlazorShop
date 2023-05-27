using BlazorShopApi.Entities;
using BlazorShopModels.DTOs;

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
    }
}

