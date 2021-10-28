using System.Linq;

using Microsoft.AspNetCore.Mvc;

using Core.Services;
using WebAPI.Models;
using System.Collections.Generic;

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
            ProductList cartItems = new ProductList
            {
                Products = this._cartServices.GetCartItems(productIds).ToList()
            };

            if (cartItems.Products.Count() != 0) return Ok(cartItems);

            return NotFound();
        }
    }
}
