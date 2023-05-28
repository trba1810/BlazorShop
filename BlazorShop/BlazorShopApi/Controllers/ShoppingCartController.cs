using BlazorShopApi.Extentions;
using BlazorShopApi.Repositories.Contracts;
using BlazorShopModels.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

namespace BlazorShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartRepository shoppingCartRepository;
        private readonly IProductRepository productRepository;

        public ShoppingCartController(IShoppingCartRepository shoppingCartRepository, IProductRepository productRepository)
        {
            this.shoppingCartRepository = shoppingCartRepository;
            this.productRepository = productRepository;
        }

        [HttpGet]
        [Route("{userId}/GetItems")]
        public async Task<ActionResult<IEnumerable<CartItemDTO>>> GetItems(int userId)
        {
            try
            {
                var cartItems = await this.shoppingCartRepository.GetItems(userId);
                if (cartItems == null)
                {
                    return NoContent();
                }
                var products = await this.productRepository.GetItems();
                if (products == null)
                {
                    throw new Exception("No product exist in system");
                }

                var cartItemsDto = cartItems.ConvertToDto(products);
                return Ok(cartItemsDto);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CartItemDTO>> GetItem(int id)
        {
            try
            {
                var cartItem = await this.shoppingCartRepository.GetItem(id);
                if(cartItem == null)
                {
                    return NotFound();
                }
                var product = await productRepository.GetItem(cartItem.ProductId);
                if(product == null)
                {
                    return NotFound();
                }

                var cartItemDto = cartItem.ConvertToDto(product);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
