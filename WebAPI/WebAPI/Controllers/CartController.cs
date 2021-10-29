using System.Linq;
using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using Core.Services;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly CartServices _cartServices;

        public CartController(CartServices cartServices)
            => this._cartServices = cartServices;

        [HttpGet]
        public IActionResult GetCartItems([FromQuery] IEnumerable<int> productIds)
        {
            var cartItems = new ProductList
            {
                Products = this._cartServices.GetCartItems(productIds).ToList()
            };

            if (cartItems.Products.Any()) return Ok(cartItems);

            return NotFound();
        }
    }
}
